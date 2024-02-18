using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Project2.General;
public class StarController : MonoBehaviour, ICollectable
{
   
    [SerializeField] private GameObject _collectEffect;
    [SerializeField] private float moveTime;
    void Start()
    {
        IdleMove();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.TryGetComponent(out PlayerInteractableController playerInteractable)){
            Collect();
            playerInteractable.CollectGem();
        }
    }

    public void Collect()
    {
        PlayEffect();
        DOTween.Kill(transform);
        transform.SetParent(GameManager.Instance.LevelCanvasManager.GetCurrentStarTransform());
        transform.DOScale(0.6f, moveTime);
        transform.DOLocalMove(Vector3.zero, moveTime).OnComplete(()=>{
            GameManager.Instance.LevelCanvasManager.FillStar();
            Debug.Log("Fill Çalıştı");
            Destroy(gameObject);
        });
    }

    public void IdleMove()
    {
        transform.DOMoveY(transform.position.y + 0.3f, 1.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void PlayEffect()
    {
        GameObject effect = Instantiate(_collectEffect, transform.position, Quaternion.identity);
    }
}
