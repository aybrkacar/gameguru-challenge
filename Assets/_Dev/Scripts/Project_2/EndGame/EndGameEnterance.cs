using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameEnterance : MonoBehaviour
{
    [SerializeField] private EndGameController _endGameController;
    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.TryGetComponent(out PlayerInteractableController playerInteractable)){
            playerInteractable.EnterEndGame(_endGameController.MovePoint);
        }
    }
}
