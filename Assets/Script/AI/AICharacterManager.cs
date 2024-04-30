using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class AICharacterManager : CharacterManager
{
    public AICharacterControlAnimator _controlAnimator;
    public AICharacterControlMovement _controlMovement;
    
    [Header("Current State")] 
    [SerializeField] private AIState currentState;

    [Title("State List")] 
    public AIStateHolder[] states;
    
    [Title("Drop Item")] 
    [SerializeField] private GameObject dropItem;
    
    private bool isDead = false;
    [HideInInspector] public float spawnDuration = 1.5f;
    [HideInInspector] public float spawnTimer;
    [HideInInspector] public bool isSpawn;
    public bool IsDead
    {
        get => isDead;
        set => isDead = value;
    }

    protected override void Awake()
    {
        base.Awake();
        currentState = GetState(Constants.AI_Spawn);
    }
    protected override void FixedUpdate()
    {
        if(isDead) return;
        base.FixedUpdate();
        if (_controlCombat.health.CurrentHp <= 0)
        {
            SwitchStateTo(GetState(Constants.AI_Dead));
        }
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
        ProcessState();
        Debug.Log("CurrentState: " + currentState);
    }

    public void InitDropItem()
    {
        Instantiate(dropItem, transform.position, quaternion.identity);
    }
    
    public AIState GetState(string name)
    {
        return Array.Find(states, stateHolder => stateHolder.name == name).state;
    }
}
