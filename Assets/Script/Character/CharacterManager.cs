using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterController _characterController;
    [HideInInspector] public Animator _animator;
    // [HideInInspector] public CharacterControlMovement _controlMovement;
    // [HideInInspector] public CharacterControlAnimator _controlAnimator;
    //[SerializeField] public CharacterControlCombat _controlCombat;
    protected virtual void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {

    }
    // protected virtual void LateUpdate()
    // {
    //
    // }
    protected virtual void FixedUpdate()
    {

    }
}
