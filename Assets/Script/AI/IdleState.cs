using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Idle")]
public class IdleState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacterManager)
    {
        Debug.Log("Idle State");
        aiCharacterManager._controlAnimator.moveAmount = 0;
        //aiCharacterManager._controlAnimator.HandleAllAnimation(0);
        return base.Tick(aiCharacterManager);
    }
}
