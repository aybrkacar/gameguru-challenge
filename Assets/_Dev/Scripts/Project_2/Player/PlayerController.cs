using System.Collections;
using UnityEngine;
using Project2.General;
using TMPro;


public class PlayerController : MonoBehaviour
{
    #region Variable
    [SerializeField] private PlayerAnimatorController playerAnimatorController;

    #endregion

    #region MonoBehaviour
    private void Start()
    {
        GameManager.OnGameStart += PlayerStart;
        
    }

    #endregion

    #region Func

    void PlayerStart(bool isOpen)
    {
        if (isOpen is true)
        {
            StartCoroutine(SpeedUpPlayerAnimation());
            CheckDeath();
        }
    }

    private IEnumerator SpeedUpPlayerAnimation()
    {
        float elapsedTime = 0;
        while (elapsedTime < 1f)
        {
            elapsedTime += 0.15f;
            playerAnimatorController.SetPlayerAnimationSpeed(elapsedTime);
            yield return new WaitForSeconds(0.1f);
        }

        playerAnimatorController.SetPlayerAnimationSpeed(1f);
    }

    public void SetIdleAnimation()
    {
        playerAnimatorController.SetPlayerAnimationSpeed(0f);
    }

    public void SetFallAnimation(){
        playerAnimatorController.TriggerDeathAnim();
    }
    public void CheckDeath(){
        StartCoroutine(DeathControl());
    }

    IEnumerator DeathControl(){
        WaitForSeconds second = new WaitForSeconds(0.1f);
        bool isAlive = true;
        while(isAlive){
            if(transform.position.y <= -1){
                isAlive = false;
                SetFallAnimation();
                GameManager.Instance.LevelEnd(false);
            }

            yield return second;
        }
    }

    #endregion

}