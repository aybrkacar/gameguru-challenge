using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GemController : MonoBehaviour, ICollectable
{
    [SerializeField] private GameObject _collectEffect;
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
