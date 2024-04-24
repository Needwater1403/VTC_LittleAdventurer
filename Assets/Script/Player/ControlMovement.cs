using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ControlMovement : CharacterControlMovement
{
    
    //private Transform tf;
    private Vector3 moveDir;
    private Vector3 rotationDir;
    private float velocityY;
    private float atkStartTime = 0;
    
    private ConfigMovementSO configMovement => ConfigCenter.Instance.GetPLayerConfigMovement();
    protected override void Awake()
    {
        //tf = transform;
    }
    
    public void HandleAllMovement() //MOVEMENT BASE ON CAMERA PERSPECTIVE
    {

        HandleAttackSlide();
        HandleRoll();
        //Grounded movement handle
        HandleGroundMovement();
        //Rotation handle
        HandleRotation();
    }

    
    private void HandleGroundMovement()
    {
        moveDir.Set(ReceiveInput.Instance.MovementInputValue.x,0,ReceiveInput.Instance.MovementInputValue.y);
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

        if (lockInRoll)
        {
            moveDir = transform.forward * configMovement.rollSpeed;
            moveDir += velocityY * Vector3.up;
        }

        if(lockInAttackState)
        {
            if (lockInAttack)
            {
                HandleSlideWhenAttack();
            }

            moveDir = Vector3.zero;
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
        if (lockInRoll || lockInAttackState) return;
        rotationDir = Vector3.zero;
        rotationDir.Set(ReceiveInput.Instance.MovementInputValue.x,0,ReceiveInput.Instance.MovementInputValue.y);
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
    
    private float lastRollTime;
    [HideInInspector] public bool lockInRoll;
    private void HandleRoll()
    {
        if(Time.realtimeSinceStartup - lastRollTime < configMovement.rollCooldown) return;
        var _roll = ReceiveInput.Instance.RollInputValue;
        if (!_roll) return;
        OnRoll();
    }
    public void OnRoll()
    {
        if (lockInRoll) return;
        lastRollTime = Time.realtimeSinceStartup;
        GameManager.Instance.Player._controlAnimator.SetRoll();
        lockInRoll = true;
    }
    
    private float lastAttackTime;
    [HideInInspector] public bool lockInAttack;
    [FormerlySerializedAs("lockInAttackPhase")] [FormerlySerializedAs("lockInAttack1")] [HideInInspector] public bool lockInAttackState;
    private void HandleAttackSlide()
    {
        if(Time.realtimeSinceStartup - lastAttackTime < configMovement.attackCooldown) return;
        var _slide = ReceiveInput.Instance.AttackInputValue;
        if (!_slide) return;
        OnAttackSlide();
    }
    public void OnAttackSlide()
    {
        lastAttackTime = Time.realtimeSinceStartup;
        GameManager.Instance.Player._controlAnimator.SetAttack();
        lockInAttack = true;
        lockInAttackState = true;
    }
}
