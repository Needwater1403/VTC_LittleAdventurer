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
    protected virtual void Awake()
    {
        _characterManager = GetComponent<CharacterManager>();
    }

    protected void UpdateAnimation(float veloX, float veloY)
    {
        _characterManager._animator.SetFloat(VelocityX, veloX);
        _characterManager._animator.SetFloat(VelocityZ, veloY);
        _characterManager._animator.SetBool(isFall,!_characterManager._characterController.isGrounded); 
    }
    protected void AIUpdateAnimation(float veloX, float veloY)
    {
        _characterManager._animator.SetFloat(VelocityX, veloX);
        _characterManager._animator.SetFloat(VelocityZ, veloY);
        _characterManager._animator.SetBool(isFall,_characterManager._characterController.isGrounded); 
    }
}
