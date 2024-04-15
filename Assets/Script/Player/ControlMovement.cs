using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlMovement : CharacterControlMovement
{
    private PlayerManager _playerManager;
    
    private Transform tf;
    public Transform TF => tf;

    private Vector2 moveValue;
    private Vector3 moveDir;
    private Vector3 rotationDir;
    private float velocityY;
    private bool canMove;
    private float atkStartTime = 0;

    
    [SerializeField] private ConfigMovementSO configMovement;
    protected override void Awake()
    {
        tf = transform;
        _playerManager = GetComponent<PlayerManager>();
    }

    private void GetMovementInputValue()
    {
        moveValue = ReceiveInput.Instance.movementInputValue;
    }
    private void GetAttackInputValue()
    {
        canMove = ReceiveInput.Instance.canMove;
    }
    public void HandleAllMovement() //MOVEMENT BASE ON CAMERA PERSPECTIVE
    {
        GetAttackInputValue();
        if (!canMove)
        {
            SlideWhenAttack();
            return;
        }
        //Grounded movement handle
        HandleGroundMovement();
        //Rotation handle
        HandleRotation();
    }

    
    private void HandleGroundMovement()
    {
        GetMovementInputValue();
        atkStartTime = 0;
        moveDir.Set(moveValue.x,0,moveValue.y);
        moveDir.Normalize();
        moveDir = Quaternion.Euler(0, -45f, 0) * moveDir;
        moveDir.y = 0;
        
        //-----------------MOVE-----------------
        if (ReceiveInput.Instance.moveAmount <= 0.5f)
        {
            moveDir *= configMovement.walkSpeed;
        }
        else if (ReceiveInput.Instance.moveAmount <= 1)
        {
            moveDir *= configMovement.runSpeed;
        }
        
        //------------HANDLE GRAVITY------------
        velocityY = !_playerManager._characterController.isGrounded ? configMovement.gravity : configMovement.gravity * 0.3f;
        moveDir += velocityY * Vector3.up;
        _playerManager._characterController.Move(moveDir * Time.deltaTime);
    }
    

    private void HandleRotation()
    {
        rotationDir = Vector3.zero;
        rotationDir.Set(moveValue.x,0,moveValue.y);
        rotationDir.Normalize();
        rotationDir = Quaternion.Euler(0, -45f, 0) * rotationDir;
        rotationDir.y = 0;
        if (rotationDir == Vector3.zero)
        {
            rotationDir = transform.forward;
        }
        var newDir = Quaternion.LookRotation(rotationDir);
        var camDir = Quaternion.Slerp(transform.rotation, newDir, configMovement.rotationSpeed * Time.deltaTime);
        transform.rotation = camDir;
    }

    private void SlideWhenAttack()
    {
        if (atkStartTime != 0) return;
        atkStartTime = Time.time;
        moveDir = Vector3.zero;
        if(Time.time < atkStartTime + configMovement.slideDuration)
        {
            var time = Time.time - atkStartTime;
            var lerpTime = time / configMovement.slideDuration;
            moveDir = Vector3.Lerp(transform.forward * configMovement.slideSpeed, Vector3.zero, lerpTime);
            moveDir += velocityY * Vector3.up;
            _playerManager._characterController.Move(moveDir);
        }
    }
}
