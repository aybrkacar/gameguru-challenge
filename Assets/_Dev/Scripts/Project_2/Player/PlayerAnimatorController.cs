using System;
using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private readonly int _blendAnim = Animator.StringToHash("forwardSpeed");
    private readonly int _dance = Animator.StringToHash("Dance");
    private readonly int _fall = Animator.StringToHash("Fall");

    private void OnEnable()
    {
        GameManager.OnMovement += StopAnimations;
    }

    private void Start() {
        GameManager.OnMovement += StopAnimations;
    }

    private void OnDisable()
    {
        GameManager.OnMovement -= StopAnimations;
    }

    public void SetPlayerAnimationSpeed(float value)
    {
        _animator.SetFloat(_blendAnim, value);
    }

    public void TriggerCheerAnimation()
    {
        _animator.SetTrigger(_dance);
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