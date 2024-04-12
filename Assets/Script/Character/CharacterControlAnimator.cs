using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlAnimator : MonoBehaviour
{
    private CharacterManager _characterManager;
    private static readonly int VelocityX = Animator.StringToHash("velocityX");
    private static readonly int VelocityZ = Animator.StringToHash("velocityZ");
    private static readonly int isFall = Animator.StringToHash("isFall");
    private static readonly int Attack = Animator.StringToHash("Attack");
    protected virtual void Awake()
    {
        _characterManager = GetComponent<CharacterManager>();
    }

    protected void UpdateAnimation(float veloX, float veloY, bool isAttacking = false)
    {
        _characterManager._animator.SetFloat(VelocityX, veloX);
        _characterManager._animator.SetFloat(VelocityZ, veloY);
        _characterManager._animator.SetBool(isFall,!_characterManager._characterController.isGrounded);
        if (!isAttacking) return;
        ReceiveInput.Instance.isAttacking = false;
        _characterManager._animator.SetTrigger(Attack);
    }
    protected void AIUpdateAnimation(float veloX, float veloY, bool isAttacking = false)
    {
        if (isAttacking)
            _characterManager._animator.SetTrigger(Attack);
        else
        {
            _characterManager._animator.SetFloat(VelocityX, veloX);
            _characterManager._animator.SetFloat(VelocityZ, veloY);
            _characterManager._animator.SetBool(isFall, _characterManager._characterController.isGrounded);
        }
    }
}
