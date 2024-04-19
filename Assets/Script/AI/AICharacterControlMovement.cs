using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AICharacterControlMovement : CharacterControlMovement
{
    [HideInInspector] public UnityEngine.AI.NavMeshAgent _navMeshAgent;
    private Transform targetTf;
    private PlayerManager _player;
    private float aggroRange = 17;
    public bool canRotate;
    protected override void Awake()
    {
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        targetTf = GameObject.FindWithTag(Constants.PlayerTag).transform;
        _player = targetTf.GetComponent<PlayerManager>();
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
        if (!targetTf || _player.IsDead) return;
        if(Vector3.Distance(targetTf.position, transform.position) <= aggroRange)
        {
            _test?.Invoke(_list[1]);
        }
    }
    public void HandleAIAttackRange(Action<AIState> _test, List<AIState> _list)
    {
        
        if (!targetTf || _player.IsDead)
        {
            _test?.Invoke(_list[0]);
            return;
        }
        if(Vector3.Distance(targetTf.position, transform.position) >= _navMeshAgent.stoppingDistance)
        {
            canRotate = false;
            _test?.Invoke(_list[1]);
        }
    }
    
    public void Rotate()
    {
        Quaternion rotation = Quaternion.LookRotation(targetTf.position - transform.position);
        transform.rotation = rotation;
    }

    private void Update()
    {
        if (canRotate)
        {
            LookAtTarget();
        }
        
    }

    public void LookAtTarget()
    {
        transform.LookAt(targetTf, Vector3.up);
    }
    
}
