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
        isAttacking = ReceiveInput.Instance.isAttacking;
    }

    public void HandleAllCombat()
    {
        if (health.CurrentHp <= 0)
        {
            
        }
    }
    
}
