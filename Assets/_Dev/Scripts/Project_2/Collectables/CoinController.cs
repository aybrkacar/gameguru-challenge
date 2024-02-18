using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinController : MonoBehaviour, ICollectable
{
    
    [SerializeField] private GameObject _collectEffect;
    void Start()
    {
        IdleMove();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.TryGetComponent(out PlayerInteractableController playerInteractable)){
            Collect();
            playerInteractable.CollectCoin();
        }
    }

    public void Collect()
    {
        PlayEffect();
        DOTween.Kill(transform);
        Destroy(gameObject);
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
