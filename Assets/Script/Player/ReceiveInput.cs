using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(ReceiveInput))]
[RequireComponent(typeof(PlayerInput))]
public class ReceiveInput : MonoBehaviour
{
    public static ReceiveInput Instance;
    public Vector2 movementInputValue;
    public Vector2 lookInputValue;
    public float moveAmount;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool canAttack = true;
    [HideInInspector] public bool canRoll = true;
    [HideInInspector] public bool startAttack;
    [HideInInspector] public bool startRoll;
    [HideInInspector] public bool isRoll = true;
    [HideInInspector] public bool isAttack = true;
    private float atkTimeCounter=0;
    private float rollTimeCounter=0;
    
    
    private void Awake()
    {
        startRoll = false; // FIX BUG: CHARACTER AUTO ROLL WHEN GAME START WITHOUT RECEIVING INPUT
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

    private void LateUpdate()
    {
        ResetAttackTime();
        ResetRollTime();
    }

    public void OnAttack(InputAction.CallbackContext _context)
    {
        if (!canAttack) return;
        canAttack = false;
        switch (_context.phase)
        {
            case InputActionPhase.Started:
                startAttack = true;
                canMove = false;
                isAttack = true;
                break;
            case InputActionPhase.Canceled:
                startAttack  = false;
                break;
        }
    }
    
    public void OnRoll(InputAction.CallbackContext _context)
    {
        if (!canRoll) return;
        canRoll = false;
        switch (_context.phase)
        {
            case InputActionPhase.Started:
                startRoll = true;
                canMove = false;
                isRoll = true;
                break;
            case InputActionPhase.Canceled:
                startRoll = false;
                break;
        }
    }

    private void ResetAttackTime()
    {
        if(!canAttack)
        {
            atkTimeCounter+= Time.deltaTime;
        }
        if (!(atkTimeCounter >= ConfigCenter.Instance.GetPLayerConfigMovement().clickAttackDuration)) return;
        canAttack = true;
        atkTimeCounter -= ConfigCenter.Instance.GetPLayerConfigMovement().clickAttackDuration;
    }
    private void ResetRollTime()
    {
        if(!canRoll)
        {
            rollTimeCounter += Time.deltaTime;
        }
        if (!(rollTimeCounter >= ConfigCenter.Instance.GetPLayerConfigMovement().clickRollDuration)) return;
        canRoll = true;
        rollTimeCounter -= ConfigCenter.Instance.GetPLayerConfigMovement().clickRollDuration;
    }
}
