using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

public class AICharacterControlAnimator : CharacterControlAnimator
{
     public float moveAmount;
    [Title("VFX")]
    [SerializeField] private VisualEffect VFX_footStep;


    public void HandleAllAnimation() 
    {
        AIUpdateAnimation(0,moveAmount);

    }

    private void FootStepBurstParticle()
    {
        VFX_footStep.Play();
    }
    protected override void Awake()
    {
        base.Awake();
    }
}
