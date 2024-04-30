using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AICharacterControlMovement : CharacterControlMovement
{
    [HideInInspector] public UnityEngine.AI.NavMeshAgent _navMeshAgent;
    private PlayerManager _player;
    public float aggroRange = 17;
    [HideInInspector] public bool canRotate;
    protected override void Awake()
    {
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _player = GameManager.Instance.Player;
    }
    
    public void HandleAIPursueMovement(Action<AIState> _test, AICharacterManager aiCharacterManager)
    {
        if(Vector3.Distance(_player.transform.position, transform.position) >= _navMeshAgent.stoppingDistance)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
        }
        else
        {
            _test?.Invoke(aiCharacterManager.GetState(Constants.AI_Attack));
        }
    }
    public void HandleAIAggroRange(Action<AIState> _test, AICharacterManager aiCharacterManager)
    {
        if (!_player.transform || _player.IsDead) return;
        if(Vector3.Distance(_player.transform.position, transform.position) <= aggroRange)
        {
            _test?.Invoke(aiCharacterManager.GetState(Constants.AI_Pursue));
        }
    }
    public void HandleAIAttackRange(Action<AIState> _test, AICharacterManager aiCharacterManager)
    {
        
        if (!_player.transform || _player.IsDead)
        {
            _test?.Invoke(aiCharacterManager.GetState(Constants.AI_Idle));
            return;
        }
        if(Vector3.Distance(_player.transform.position, transform.position) >= _navMeshAgent.stoppingDistance)
        {
            canRotate = false;
            _test?.Invoke(aiCharacterManager.GetState(Constants.AI_Pursue));
        }
    }
    
    public void Rotate()
    {
        Quaternion rotation = Quaternion.LookRotation(_player.transform.position - transform.position);
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
        if (_player == null) return;
        transform.LookAt(_player.transform, Vector3.up);
    }
    
}
