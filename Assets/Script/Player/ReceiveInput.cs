using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(ReceiveInput))]
[RequireComponent(typeof(PlayerInput))]
public class ReceiveInput : MonoBehaviour
{
    public static ReceiveInput Instance;

    #region Parameters

    public Vector2 movementInputValue;
    public Vector2 MovementInputValue => movementInputValue;
    
    private Vector2 lookInputValue;
    public Vector2 LookInputValue => lookInputValue;
    public float moveAmount;

    private bool attackInputValue;
    public bool AttackInputValue => attackInputValue;
    
    private bool rollInputValue;
    public bool RollInputValue => rollInputValue;

    #endregion

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        movementInputValue = _context.ReadValue<Vector2>();
        moveAmount = Mathf.Clamp01(Mathf.Abs(movementInputValue.x) + Mathf.Abs((movementInputValue.y)));

        //CHANGE BETWEEN WALK AND RUN ANIMATION
        switch (moveAmount)
        {
            case <= 0.5f and > 0:
                moveAmount = 0.5f;
                break;
            case <= 1 and > 0.5f:
                moveAmount = 1;
                break;
        }
    }
    
    public void OnAttack(InputAction.CallbackContext _context)
    {
        switch (_context.phase)
        {
            case InputActionPhase.Started:
                attackInputValue = true;
                break;
            case InputActionPhase.Canceled:
                attackInputValue  = false;
                break;
        }
    }
    
    public void OnRoll(InputAction.CallbackContext _context)
    {
        switch (_context.phase)
        {
            case InputActionPhase.Started:
                rollInputValue = true;
                break;
            case InputActionPhase.Canceled:
                rollInputValue = false;
                break;
        }
    }
}
