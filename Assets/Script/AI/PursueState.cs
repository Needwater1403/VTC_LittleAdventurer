using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Pursue")]
public class PursueState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacterManager)
    {
        Debug.Log("Pursue State");
        aiCharacterManager._controlAnimator.moveAmount = 1;
        //aiCharacterManager._controlAnimator.HandleAllAnimation(0);
        aiCharacterManager._controlMovement.HandleAIPursueMovement(aiCharacterManager.SwitchStateTo, 
            aiCharacterManager.stateList);
        return base.Tick(aiCharacterManager);
        // else
        // {
        //     Debug.Log("Pursue State -> IdLE");
        //     aiCharacterManager._controlMovement.a = true;
        //     return aiCharacterManager.stateList[0].Tick(aiCharacterManager);
        // }
    }
}
