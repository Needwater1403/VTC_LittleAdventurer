using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

public class ControlAnimator : CharacterControlAnimator
{
    public float moveAmount;
    private bool isAttacking;
    private bool isRoll;
    [HideInInspector] public bool isDead;
    [Title("VFX")]
    [SerializeField] private VisualEffect VFX_footStep;
    [SerializeField] private ParticleSystem VFX_sword1;
    [SerializeField] private ParticleSystem VFX_sword2;
    [SerializeField] private ParticleSystem VFX_sword3;
    [SerializeField] private VisualEffect VFX_slash;
    [SerializeField] private VisualEffect VFX_Heal;
    [SerializeField] private VisualEffect VFX_PickUpCoin;
    private void GetMovementInputValue(bool isPaused)
    {
        if (isPaused)
        {
            moveAmount = 0;
            return;
        }
        moveAmount = ReceiveInput.Instance.moveAmount;
    }
    private void GetAttackInputValue()
    {
        isAttacking = ReceiveInput.Instance.startAttack;
    }
    private void GetRollInputValue()
    {
        isRoll = ReceiveInput.Instance.startRoll;
    }
    public void HandleAllAnimation(bool isPaused)
    {
        GetMovementInputValue(isPaused);
        GetAttackInputValue();
        GetRollInputValue();
        UpdateAnimation(0,moveAmount, isDead, isAttacking, isRoll);
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
    private void PlaySword2VFX()
    {
        VFX_sword2.Play();
    }
    private void PlaySword3VFX()
    {
        VFX_sword3.Play();
    }
    
    public void SlashVFX(Vector3 _pos)
    {
        VFX_slash.transform.position = _pos;
        VFX_slash.Play();
    }

    public void HealVFX()
    {
        VFX_Heal.Play();
    }
    public void PickUpCoinVFX()
    {
        VFX_PickUpCoin.Play();
    }
    #endregion
    
    
    private void EndAnimation()
    {
        ReceiveInput.Instance.canMove = true;
        ReceiveInput.Instance.isRoll = false;
        ReceiveInput.Instance.isAttack = false;
    }
    protected override void Awake()
    {
        base.Awake();
    }
}

