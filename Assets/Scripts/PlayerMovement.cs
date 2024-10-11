using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody2D body;
    private Animator animator;
    private bool isGrounded;

    // Is called when script instance is loaded
    private void Awake()
    {
        // Script will check the Player Object has as RigidBody2D
        body = GetComponent<Rigidbody2D>();
        
        // Gets Animator component
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);
        
        // Ensures that if the Player is facing to the Right,
        // the sprite is facing Right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Ensures that if the Player is facing to the Left,
        // the sprite is flipped (facing left)
        if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Uses whatever unity has set as "Jump" as the input
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
            Jump();
        
        // Set animator parameters
        animator.SetBool("Running", horizontalInput != 0);
        animator.SetBool("Grounded", isGrounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        animator.SetTrigger("Jump");
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
