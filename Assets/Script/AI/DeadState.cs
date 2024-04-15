using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Dead")]
public class DeadState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacterManager)
    {
        if(!aiCharacterManager._controlAnimator.isDead )
        {
            aiCharacterManager.IsDead = true;
            aiCharacterManager._controlAnimator.isDead = true;
        }
        return base.Tick(aiCharacterManager);
    }
}
