using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

public class ControlAnimator : CharacterControlAnimator
{
    private float moveAmount;
    private bool isAttacking;
    [Title("VFX")]
    [SerializeField] private VisualEffect VFX_footStep;
    [SerializeField] private ParticleSystem VFX_sword1;
    [SerializeField] private VisualEffect VFX_slash;
    private void GetMovementInputValue()
    {
        moveAmount = ReceiveInput.Instance.moveAmount;
    }
    private void GetAttackInputValue()
    {
        isAttacking = ReceiveInput.Instance.isAttacking;
    }
    public void HandleAllAnimation()
    {
        GetMovementInputValue();
        GetAttackInputValue();
        UpdateAnimation(0,moveAmount, isAttacking);
        UpdateVFX();
    }

    #region VFX

    private void UpdateVFX()
    {
        if (moveAmount != 0)
        {
            VFX_footStep.Play();
        }
        else
        {
            VFX_footStep.Stop();
        }
    }

    private void PlaySword1VFX()
    {
        VFX_sword1.Play();
    }
    
    public void SlashVFX(Vector3 _pos)
    {
        VFX_slash.transform.position = _pos;
        VFX_slash.Play();
        Debug.Log("Nora1" );
    }
    #endregion
    
    
    private void EndATK()
    {
        ReceiveInput.Instance.canMove = true;
    }
    protected override void Awake()
    {
        base.Awake();
    }
    
}

