using System.Collections;
using System.Collections.Generic;
using Project2.General;
using UnityEngine;
using DG.Tweening;

public class PlayerInteractableController : MonoBehaviour
{
    public void EnterEndGame(Transform movePoint)
    {
        GameManager.Instance.LevelEnd(true);

        transform.DOMove(movePoint.position, 1f).OnComplete(() => {
            PlayerAnimatorController playerAnimatorController = GetComponent<PlayerAnimatorController>();
            playerAnimatorController.PlayDanceAnimation();
        });
    }

    public void CollectGem(){
        Debug.Log("Gem Collected.");
    }
}
