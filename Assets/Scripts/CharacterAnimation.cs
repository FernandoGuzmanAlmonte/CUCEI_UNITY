using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{

    //
    private Animator animator;
    private Rigidbody rigidBody;
    private float maxSpeed = 5f;
    
    //
    private static readonly int Speed = Animator.StringToHash("Speed");

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(Speed, rigidBody.velocity.magnitude / maxSpeed);
    }
}
