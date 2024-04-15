using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

public class AICharacterControlAnimator : CharacterControlAnimator
{
    public float moveAmount;
    public bool isAttacking;
    public bool canMove;
    public bool isDead;
    [Title("VFX")]
    [SerializeField] private VisualEffect VFX_footStep;
    [SerializeField] private VisualEffect VFX_attack; 
    public void HandleAllAnimation()
    {
        AIUpdateAnimation(0,moveAmount, isDead, isAttacking);
    }

    private void FootStepBurstParticle()
    {
        VFX_footStep.Play();
    }

    private void AttackParticle()
    {
        VFX_attack.Play();
    }
    
    protected override void Awake()
    {
        base.Awake();
        canMove = true;
        isAttacking = false;
        isDead = false;
    }
}
