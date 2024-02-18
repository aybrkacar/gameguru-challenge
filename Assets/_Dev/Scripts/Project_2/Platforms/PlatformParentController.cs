using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using Project2.General;

public class PlatformParentController : MonoBehaviour
{
    #region Variables
    [SerializeField] private LeanGameObjectPool _pool;
    [SerializeField] private int _spawnedPlatformsCount = 0;

    public float[] SpawnPoints = new float[] { -7f, 7f };
    //[HideInInspector]
    public GameObject CurrentPlatform;
    //[HideInInspector]
    public GameObject PreviousPlatform;

    [Space(5)]
    [Header("Level")]
    [SerializeField] private GameObject _basePlatform;
    [SerializeField] private GameObject _endGameChecker;

    [Space(5)]
    [Header("Materials")]
    public List<Material> _platformMatList;

    private int _materialIndex = 0;
    public int MaterialIndex
    {
        get { return _materialIndex; }
        set
        {
            value %= _platformMatList.Count;
            _materialIndex = value;
        }
    }


    #endregion

    #region Mono
    private void OnEnable() {
        LevelManager.OnLevelStarted += SetupLevel;
    }
    void Start()
    {   
        
        CurrentPlatform = _basePlatform;
        SpawnPlatform(true);
    }

    private void OnDisable() {
        LevelManager.OnLevelStarted -= SetupLevel;
    }

    #endregion

    #region Methods
    private Vector3 CalculateSpawnPos(bool isFirstPlatform)
    {
        MeshRenderer renderer = CurrentPlatform.GetComponent<MeshRenderer>();
        float zPos = CurrentPlatform.transform.position.z + renderer.bounds.size.z;

        float xPos;
        if (isFirstPlatform)
        {
            xPos = RandomizeFirstXPos();
        }
        else
        {
            xPos = SpawnPoints[CalculateLeftOrRight()];
        }

        return new Vector3(xPos, CurrentPlatform.transform.position.y, zPos);
    }

    int CalculateLeftOrRight()
    {
        PlatformController platformController = CurrentPlatform.GetComponent<PlatformController>();

        if (platformController.StartPosType == StartPosType.Left)
        {
            return 1;
        }
        return 0;
    }

    float RandomizeFirstXPos()
    {
        var randomfloat = UnityEngine.Random.Range(0, 1);

        if (randomfloat == 0)
        {
            return SpawnPoints[0];
        }

        return SpawnPoints[1];
    }

    float GetCurrentPlatformSizeX()
    {
        return CurrentPlatform.GetComponent<MeshRenderer>().bounds.size.x;
    }

    public void SpawnPlatform(bool isFirstPlatform)
    {
        if(_spawnedPlatformsCount >= LevelManager.Instance.CurrentLevelData.LevelPlatformCount) return;
        GameObject platform = _pool.Spawn(CalculateSpawnPos(isFirstPlatform), Quaternion.identity, transform);
        _spawnedPlatformsCount++;
        PlatformController platformController = platform.GetComponent<PlatformController>();
        platformController.SetupPlatform(this, isFirstPlatform, GetCurrentPlatformSizeX(), GetNextMaterial());
        PreviousPlatform = CurrentPlatform;
        CurrentPlatform = platform;
    }

    private Material GetNextMaterial(){
        var material = _platformMatList[MaterialIndex];
        MaterialIndex++;
        return material;
    }
    #endregion

    #region Level Methods
    void SetupLevel(){
        SetCheckerPos();
    }

    void SetCheckerPos(){
        var platformCount = LevelManager.Instance.CurrentLevelData.LevelPlatformCount;
        _endGameChecker.transform.position = _endGameChecker.transform.position + _basePlatform.transform.localScale.z * platformCount * Vector3.forward;
    }
    #endregion


}
