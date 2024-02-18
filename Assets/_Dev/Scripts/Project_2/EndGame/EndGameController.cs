using System.Collections;
using System.Collections.Generic;
using Project2.General;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    public Transform MovePoint;
    public PlatformParentController PlatformParentController;
    public GameObject BasePlatform;

    private void Start() {
        PlatformParentController = transform.parent.GetComponent<PlatformParentController>();
        LevelManager.OnLevelCompleted += LevelCompleted;
    }

    public void LevelCompleted(){
        PlatformParentController.CurrentPlatform = BasePlatform;
        PlatformParentController.BasePlatform = BasePlatform;
        PlatformParentController.PreviousPlatform = BasePlatform;
        LevelManager.OnLevelCompleted -= LevelCompleted;
    }
}   
