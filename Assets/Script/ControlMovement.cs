using System;
using System.Collections;
using System.Collections.Generic;
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
    private static float gravity = -9.8f;

    [SerializeField] private ConfigMovementSO configMovement;
    protected override void Awake()
    {
        tf = transform;
        _playerManager = GetComponent<PlayerManager>();
    }

    public void Move(Vector2 _vector2)
    {
        var _current = tf.position;
        var _new = new Vector3(_current.x + _vector2.x, _current.y, _current.z + _vector2.y);
        tf.position = Vector3.Lerp(_current, _new, configMovement.walkSpeed * Time.deltaTime);
        if (_vector2.x != 0 || _vector2.y != 0)
        {
            //tf.rotation = Quaternion.Euler(0f, (float)(System.Math.Atan2((_vector2.x - 0), (_vector2.y - 0)) * 180 / 3.14), 0f);
        }
    }

    public void GetMovementInputValue()
    {
        moveValue = ReceiveInput.Instance.movementInputValue;
    }
    public void HandleAllMovement() //MOVEMENT BASE ON CAMERA PERSPECTIVE
    {
        //Grounded movement handle
        HandleGroundMovement();
        //Aerial movement handle
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

        //------------HANDLE GRAVITY------------
        velocityY = !_playerManager._characterController.isGrounded ? gravity : gravity * 0.3f;
        moveDir += velocityY * Vector3.up;
        
        //-----------------MOVE-----------------
        if (ReceiveInput.Instance.moveAmount <= 0.5f)
        {
            _playerManager._characterController.Move(moveDir * (configMovement.walkSpeed * Time.deltaTime));
        }
        else if (ReceiveInput.Instance.moveAmount <= 1)
        {
            _playerManager._characterController.Move(moveDir * (configMovement.runSpeed * Time.deltaTime));
        }

        
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
}
