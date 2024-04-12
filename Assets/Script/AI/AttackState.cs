using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Attack")]
public class AttackState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacterManager)
    {
        Debug.Log("Attack State");
        // HANDLE ATTACK ANIMATION
        aiCharacterManager._controlAnimator.moveAmount = 0;
        aiCharacterManager._controlAnimator.isAttacking = true;
        // HANDLE ATTACK (CONTROL COMBAT SCRIPT)
        //
        //SWITCH TO PURSUE STATE
        if(!aiCharacterManager._controlAnimator.canMove)
        {
            aiCharacterManager._controlMovement.HandleAIAggroRange(aiCharacterManager.SwitchStateTo,
                aiCharacterManager.stateList);
        }
        return base.Tick(aiCharacterManager);
    }
}
