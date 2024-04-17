using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Attack")]
public class AttackState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacterManager)
    {
        
        if(!aiCharacterManager._controlAnimator.canMove)
        {
            aiCharacterManager._controlMovement.HandleAIAttackRange(aiCharacterManager.SwitchStateTo,
                aiCharacterManager.stateList);
        }
        // HANDLE ATTACK ANIMATION
        if(!aiCharacterManager._controlAnimator.isAttacking && aiCharacterManager._controlAnimator.canMove)
        {
            aiCharacterManager._controlMovement.LookAtTarget();
            aiCharacterManager._controlAnimator.isAttacking = true;
        }
        // HANDLE ATTACK (CONTROL COMBAT SCRIPT)
        //
        //SWITCH TO PURSUE STATE
        return base.Tick(aiCharacterManager);
    }
}
