using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterControlMovement : CharacterControlMovement
{
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    private Transform targetTf;
    public float aggroRange = 10;
    public float attackRange = 1;
    protected override void Awake()
    {
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        targetTf = GameObject.FindWithTag("Player").transform;
    }
    
    public void HandleAIPursueMovement(Action<AIState> _test, List<AIState> _list)
    {
        if(Vector3.Distance(targetTf.position, transform.position) >= _navMeshAgent.stoppingDistance)
        {
            _navMeshAgent.SetDestination(targetTf.position);
        }
        else
        {
            _test?.Invoke(_list[2]);
        }
    }
    public void HandleAIAggroRange(Action<AIState> _test, List<AIState> _list)
    {
        if(Vector3.Distance(targetTf.position, transform.position) <= aggroRange)
        {
            _test?.Invoke(_list[1]);
        }
    }
    public void HandleAIAttackRange(Action<AIState> _test, List<AIState> _list)
    {
        if(Vector3.Distance(targetTf.position, transform.position) >= _navMeshAgent.stoppingDistance)
        {
            _test?.Invoke(_list[1]);
        }
    }
    
}
