using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float turnSpeed = 100f;

    private Animator animator;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Axis Variables
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        //
        Vector3 velocity = CalculateVelocity(zAxis);
        Vector3 rotation = CalculateRotation(xAxis);
        
        UpdateAnimator(xAxis, zAxis);
        MoveCharacter(velocity);
    }

    //
    private Vector3 CalculateVelocity(float zAxis)
    {
        Vector3 velocity = new Vector3(0.0f, 0.0f, (zAxis * moveSpeed));
        return velocity;
    }
    
    //
    private Vector3 CalculateRotation(float xAxis)
    {
        Vector3 rotation = new Vector3(0.0f, 0.0f, (xAxis * turnSpeed));
        return rotation;
    }
    
    //
    private void MoveCharacter(Vector3 velocity)
    {
        transform.Translate(velocity * Time.deltaTime);
    }

    //
    private void UpdateAnimator(float xAxis, float zAxis)
    {
        // Arrow Variables
        bool downArrow = Input.GetKey(KeyCode.DownArrow);
        bool upArrow = Input.GetKey(KeyCode.UpArrow);
        bool rightArrow = Input.GetKey(KeyCode.RightArrow);
        bool leftArrow = Input.GetKey(KeyCode.LeftArrow);
        
        //
        if (upArrow && zAxis > 0)
        {
            animator.SetBool(IsWalking, true);
            animator.SetFloat(Speed, 1.0f);
        }
        else if (downArrow && zAxis < 0)
        {
            animator.SetBool(IsWalking, true);
            animator.SetFloat(Speed, -1.0f);
        }
        else if (rightArrow && xAxis > 0)
        {
            
        }
        else if (leftArrow && xAxis < 0)
        {
            
        }
        else
        {
            animator.SetBool(IsWalking, false);
        }
    }
}