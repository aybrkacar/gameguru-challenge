using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using General;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlatformController : MonoBehaviour
{
    #region Variables
    public PlatformParentController _platformParentController;
    public StartPosType StartPosType;

    //Tween Settings
    [SerializeField] private float _tweenTime;
    [SerializeField] private Ease _easeType;
    [SerializeField] private bool _isBasePlatform;
    private bool _isStopped;
    #endregion

    #region Mono
    private void OnEnable()
    {
        if(_isBasePlatform) return;
        GameManager.OnPlayerTouch += StopPlatform;
    }

    private void OnDisable()
    {
        if(_isBasePlatform) return;
        GameManager.OnPlayerTouch -= StopPlatform;
    }

    #endregion

    #region Methods
    public void SetupPlatform(PlatformParentController platformParentController, bool isFirstPlatform, float sizeX, Material mat)
    {
        _platformParentController = platformParentController;
        SetScale(sizeX);
        SetStartType();
        SetMaterial(mat);

        MovePlatform(isFirstPlatform);
    }
    //TRANSFORM
    public void SetScale(float sizeX)
    {
        transform.localScale = new Vector3(sizeX, transform.localScale.y, transform.localScale.z);
    }

    void SetStartType()
    {
        if (transform.position.x <= 0)
        {
            StartPosType = StartPosType.Left;
        }
        else
        {
            StartPosType = StartPosType.Right;
        }
    }

    void SetMaterial(Material mat)
    {
        transform.GetComponent<MeshRenderer>().material = mat;
    }

    //MOVEMENT
    public void MovePlatform(bool isFirstPlatform)
    {
        if (!_platformParentController) return;

        if (StartPosType == StartPosType.Left)
        {
            if (isFirstPlatform)
            {
                MoveTween(_platformParentController.SpawnPoints[1], -1);
            }
            else
            {
                MoveTween(_platformParentController.SpawnPoints[1], 1);
            }
        }
        else
        {
            if (isFirstPlatform)
            {
                MoveTween(_platformParentController.SpawnPoints[0], -1);
            }
            else
            {
                MoveTween(_platformParentController.SpawnPoints[0], 1);
            }
        }
    }

    void MoveTween(float xPos, int loop)
    {
        transform.DOMoveX(xPos, _tweenTime).SetLoops(loop, LoopType.Yoyo).SetEase(_easeType);
    }

    //WHEN TOUCH
    public void StopPlatform()
    {
        if(_isStopped) return;
        _isStopped = true;

        DOTween.Kill(transform);
        CheckPlatform();
        
    }

    void CheckPlatform()
    {
        Debug.Log(gameObject.name + " çalıştı" );
        Transform prevPlatform = _platformParentController.PreviousPlatform.transform;

        var prevSize = prevPlatform.localScale.x;
        var distance = prevPlatform.position.x - transform.position.x;

        if (Mathf.Abs(distance) <= GameManager.Instance.DifficultyData.ToleranceDistance)
        {
            Perfection(prevPlatform);
        }
        else
        {
            if (distance >= prevSize)
            {
                Fail();
            }
            else
            {
                SlicePlatform(distance);
            }
        }
        //GameManager.OnPlayerTouch -= StopPlatform;

    }

    void Perfection(Transform prevPlatform)
    {
        transform.position = new Vector3(prevPlatform.position.x, transform.position.y, transform.position.z);
        _platformParentController.SpawnPlatform(false);
        Debug.Log("çalıştı2");
        //TODO: Effect & Audio.
    }

    void Fail()
    {
        gameObject.AddComponent<Rigidbody>();
        GameManager.Instance.NullInGameTouch();
    }

    void SlicePlatform(float distance)
    {
        float direction = distance > 0 ? -1f : 1f;
        float newSize = _platformParentController.PreviousPlatform.transform.localScale.x - Mathf.Abs(distance);
        if (newSize <= 0)
        {
            Fail();
            return;
        }

        float fallPlatformSize = transform.localScale.x - newSize;
        Debug.Log("fall size:" + fallPlatformSize);

        float newXPosition = _platformParentController.PreviousPlatform.transform.position.x - (distance / 2);
        transform.localScale = new Vector3(newSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);



        float cubeEdge = transform.position.x + (newSize / 2f * direction);
        float fallPlatformXPos = cubeEdge + fallPlatformSize / 2f * direction;

        SpawnFallPlatform(fallPlatformSize, fallPlatformXPos);

        _platformParentController.SpawnPlatform(false);
    }


    private void SpawnFallPlatform(float fallPlatformSize, float fallPlatformXPos)
    {
        var fallPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        SetUpFallPlatform(fallPlatform, fallPlatformSize, fallPlatformXPos);
    }

    void SetUpFallPlatform(GameObject platform, float fallPlatformSize, float fallPlatformXPos)
    {
        platform.transform.localScale = new Vector3(fallPlatformSize, transform.localScale.y, transform.localScale.z);
        platform.transform.position = new Vector3(fallPlatformXPos, transform.position.y, transform.position.z);

        platform.GetComponent<MeshRenderer>().material = transform.GetComponent<MeshRenderer>().material;

        platform.AddComponent<Rigidbody>();
        Destroy(platform, 1f);
    }
    #endregion
}

public enum StartPosType
{
    Left,
    Right
}
