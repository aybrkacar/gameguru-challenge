using System;
using System.Collections;
using System.Collections.Generic;
using Project2.General;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private readonly int _blendAnim = Animator.StringToHash("forwardSpeed");
    private readonly int _dance = Animator.StringToHash("Dance");
    private readonly int _fall = Animator.StringToHash("Fall");

    private void OnEnable()
    {
        //GameManager.OnMovement += StopAnimations;
        LevelManager.OnLevelStarted += StopDanceAnimation;
    }

    private void Start() {
        //GameManager.OnMovement += StopAnimations;
    }

    private void OnDisable()
    {
        //GameManager.OnMovement -= StopAnimations;
        LevelManager.OnLevelStarted -= StopDanceAnimation;
    }

    public void SetPlayerAnimationSpeed(float value)
    {
        _animator.SetFloat(_blendAnim, value);
    }

    public void PlayDanceAnimation()
    {
        _animator.SetBool(_dance, true);
    }
    public void StopDanceAnimation(){
        _animator.SetBool(_dance, false);
        SetPlayerAnimationSpeed(0);
    }

    public void TriggerDeathAnim()
    {
        _animator.SetTrigger(_fall);
    }

    private void StopAnimations(bool isMove)
    {
        if (isMove is false)
        {
            SetPlayerAnimationSpeed(0);
            TriggerDeathAnim();
        }
    }
}