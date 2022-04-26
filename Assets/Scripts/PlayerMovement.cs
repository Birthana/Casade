using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpVelocity;
    public float fallMultiplier = 3.0f;
    public float lowMultiplier = 2.0f;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isJumping;
    private bool isMoving;

    private string currentState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeAnimation("Player_Idle");
    }

    private void Update()
    {
        CheckHorizontalMovement();
        CheckJumping();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        Fall();
    }

    private void CheckHorizontalMovement()
    {
        isMoving = true;
    }

    private void Move()
    {
        if (isMoving)
        {
            float h = Input.GetAxis("Horizontal");
            Vector2 movementVector = new Vector2(h, 0).normalized * speed;
            movementVector.y = rb.velocity.y;
            rb.velocity = movementVector;
            if (h == 0 && currentState != "Player_Jump")
                ChangeAnimation("Player_Idle");
            else if(currentState != "Player_Jump")
                ChangeAnimation("Player_Run");
            if (h < 0)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else if(h > 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            isMoving = false;
        }
    }

    private void CheckJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            ChangeAnimation("Player_Jump");
            isJumping = true;
        }
    }

    private void Jump()
    {
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            isJumping = false;
            Invoke("JumpComplete", 0.5f);
        }
    }

    private void JumpComplete()
    {
        ChangeAnimation("Player_Idle");
    }

    private void Fall()
    {
        if (rb.velocity.y < 0)
            rb.gravityScale = fallMultiplier;
        else if (rb.velocity.y > 0 && (!Input.GetKey(KeyCode.Space) || !Input.GetKey(KeyCode.W)))
            rb.gravityScale = lowMultiplier;
        else
            rb.gravityScale = 1.0f;
    }

    private void ChangeAnimation(string newAnimation) 
    {
        if (currentState == newAnimation)
            return;
        anim.Play(newAnimation);
        currentState = newAnimation;
    }

}
