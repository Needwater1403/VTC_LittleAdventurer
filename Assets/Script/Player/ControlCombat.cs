using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCombat : CharacterControlCombat
{
    private bool isAttacking;
    protected override void Awake()
    {
        base.Awake();
    }
    private void GetAttackInputValue()
    {
        isAttacking = ReceiveInput.Instance.startAttack;
    }

    public void HandleAllCombat()
    {
        if (health.CurrentHp <= 0)
        {
            
        }
    }
    
}
