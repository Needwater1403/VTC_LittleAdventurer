using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Attack")]
public class AttackState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacterManager)
    {
        Debug.Log("Attack State");
        // HANDLE ATTACK
        aiCharacterManager._controlAnimator.moveAmount = 0;
        return base.Tick(aiCharacterManager);
    }
}
