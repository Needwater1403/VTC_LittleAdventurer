using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AICharacterManager : CharacterManager
{
    [SerializeField] public AICharacterControlAnimator _controlAnimator;
    [SerializeField] public AICharacterControlMovement _controlMovement;
    [Header("Current State")] 
    [SerializeField] private AIState currentState;
    [Title("State List")] 
    public List<AIState> stateList;
    
    protected override void Awake()
    {
        base.Awake();
        currentState = stateList[1];
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        ProcessState();
    }
    protected override void Update()
    {
        base.Update();
        if (_controlAnimator)
        {
            _controlAnimator.HandleAllAnimation();
        }
    }
    private void ProcessState()
    {
        currentState.Tick(this);
    }

    public void SwitchStateTo(AIState _state)
    {
        currentState = _state;
        Debug.LogError("CurrentState: " + currentState);
    }
}
