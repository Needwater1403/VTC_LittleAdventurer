using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

public class ControlAnimator : CharacterControlAnimator
{
    private float moveAmount;
    [Title("VFX")]
    [SerializeField] private VisualEffect VFX_footStep;

    private void GetMovementInputValue()
    {
        moveAmount = ReceiveInput.Instance.moveAmount;
    }
    public void HandleAllAnimation()
    {
        GetMovementInputValue();
        UpdateAnimation(0,moveAmount);
        UpdateVFX();
    }

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
    protected override void Awake()
    {
        base.Awake();
    }
}

