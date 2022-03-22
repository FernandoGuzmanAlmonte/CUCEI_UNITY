using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{

    // Input Fields
    private ThirdPersonActionAssets playerActionsAsset;
    private InputAction move;
    
    // Movement Fields
    private Rigidbody rigidbody;
    [SerializeField] private float movementForce = 1f;
    // [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;
    
    // Camera
    [SerializeField] private Camera playerCamera;
    
    // Animator
    private Animator animator;

    // Awake
    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        playerActionsAsset = new ThirdPersonActionAssets();
        animator = this.GetComponent<Animator>();
    }

    //
    private void OnEnable()
    {
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
    }
    
    //
    private void OnDisable()
    {
        playerActionsAsset.Player.Disable();
    }
    
    //
    private void FixedUpdate()
    {
        forceDirection += GetCameraRight(playerCamera) * (move.ReadValue<Vector2>().x * movementForce);
        forceDirection += GetCameraForward(playerCamera) * (move.ReadValue<Vector2>().y * movementForce);
        
        rigidbody.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rigidbody.velocity.y < 0f)
        {
            rigidbody.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        Vector3 horizontalVelocity = rigidbody.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > (maxSpeed * maxSpeed))
        {
            rigidbody.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rigidbody.velocity.y;
        }
        
        LookAt();
    }

    //
    private void LookAt()
    {
        Vector3 direction = rigidbody.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            this.rigidbody.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    //
    private static Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }
    
    //
    private static Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }
}
