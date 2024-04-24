using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlMovement : CharacterControlMovement
{
    
    //private Transform tf;
    private Vector2 moveValue;
    private Vector3 moveDir;
    private Vector3 rotationDir;
    private float velocityY;
    private bool canMove;
    private bool isRoll;
    private bool isAttack;
    private float atkStartTime = 0;
    
    private ConfigMovementSO configMovement => ConfigCenter.Instance.GetPLayerConfigMovement();
    protected override void Awake()
    {
        //tf = transform;
    }

    private void GetMovementInputValue()
    {
        moveValue = ReceiveInput.Instance.movementInputValue;
    }
    private void GetAttackInputValue()
    {
        canMove = ReceiveInput.Instance.canMove;
        isAttack = ReceiveInput.Instance.isAttack;
    }
    private void GetRollInputValue()
    {
        isRoll = ReceiveInput.Instance.isRoll;
    }
    public void HandleAllMovement() //MOVEMENT BASE ON CAMERA PERSPECTIVE
    {
        GetAttackInputValue();
        GetRollInputValue();
        if (!canMove && isAttack)
        {
            HandleSlideWhenAttack();
            return;
        }
        if (!canMove && isRoll)
        {
            HandleRoll();
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
        velocityY = !GameManager.Instance.Player._characterController.isGrounded ? configMovement.gravity : configMovement.gravity * 0.3f;
        moveDir += velocityY * Vector3.up;
        GameManager.Instance.Player._characterController.Move(moveDir * Time.deltaTime);
    }

    public void ResetAtkStartTime()
    {
        atkStartTime = 0;
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

    private void HandleSlideWhenAttack()
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
            GameManager.Instance.Player._characterController.Move(moveDir);
        }
    }

    private void HandleRoll()
    {
        moveDir = transform.forward * configMovement.rollSpeed;
        moveDir += velocityY * Vector3.up;
        GameManager.Instance.Player._characterController.Move(moveDir * Time.deltaTime);
    }
}
