using System.Collections;
using System.Collections.Generic;
using Project2.General;
using UnityEngine;
using DG.Tweening;
using GameData;

public class PlayerInteractableController : MonoBehaviour
{
    public void EnterEndGame(Transform movePoint)
    {
        GameManager.Instance.LevelEnd(true);

        GetComponent<MovementController>().ResetHorizontal();
        transform.DOMove(movePoint.position, 1f).OnComplete(() => {
            PlayerAnimatorController playerAnimatorController = GetComponent<PlayerAnimatorController>();
            playerAnimatorController.PlayDanceAnimation();
        });
    }

    public void CollectGem(){
        Debug.Log("Gem Collected.");
    }

    public void CollectStar(){
        Debug.Log("Star Collected.");
    }

    public void CollectCoin(){
        SaveData.MoneyValue ++;
        GameManager.Instance.LevelCanvasManager.UpdateMoney();
    }
}
