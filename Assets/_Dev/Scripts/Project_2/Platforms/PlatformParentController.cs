using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using System;
using System.Linq;
using General;

public class PlatformParentController : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject _basePlatform;
    [SerializeField] private LeanGameObjectPool _pool;
    [SerializeField] private int _spawnedPlatformsCount = 0;

    public float[] SpawnPoints = new float[] { -7f, 7f };
    //[HideInInspector]
    public GameObject CurrentPlatform;
    //[HideInInspector]
    public GameObject PreviousPlatform;

    [Space(5)]
    [Header("Materials")]
    public List<Material> platformMatList;
    private int _materialIndex = 0;
    public int MaterialIndex
    {
        get { return _materialIndex; }
        set
        {
            value %= platformMatList.Count;
            _materialIndex = value;
        }
    }


    #endregion

    #region Mono
    void Start()
    {
        CurrentPlatform = _basePlatform;
        SpawnPlatform(true);
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
        GameObject platform = _pool.Spawn(CalculateSpawnPos(isFirstPlatform), Quaternion.identity, transform);

        PlatformController platformController = platform.GetComponent<PlatformController>();
        platformController.SetupPlatform(this, isFirstPlatform, GetCurrentPlatformSizeX(), GetNextMaterial());
        PreviousPlatform = CurrentPlatform;
        CurrentPlatform = platform;
    }

    private Material GetNextMaterial(){
        var material = platformMatList[MaterialIndex];
        MaterialIndex++;
        return material;
    }
    #endregion


}
