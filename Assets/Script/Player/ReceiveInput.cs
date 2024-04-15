using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(ReceiveInput))]
[RequireComponent(typeof(PlayerInput))]
public class ReceiveInput : MonoBehaviour
{
    public static ReceiveInput Instance;
    [HideInInspector] public PlayerManager playerManager;
    public Vector2 movementInputValue;
    public Vector2 lookInputValue;
    public float moveAmount;
    public bool isAttacking = false;
    public bool canMove = true;
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

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
        if (!canMove) return;
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
    
    public void OnLook(InputAction.CallbackContext _context)
    {
        lookInputValue  = _context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext _context)
    {
        switch (_context.phase)
        {
            case InputActionPhase.Started:
                isAttacking = true;
                canMove = false;
                break;
            case InputActionPhase.Canceled:
                isAttacking  = false;
                break;
        }
    }
}
