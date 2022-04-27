using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float jumpVelocity;
    public float fallMultiplier = 3.0f;
    public float lowMultiplier = 2.0f;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isJumping;
    private bool isMoving;
    private bool isGrounded = true;

    [Header("Attack")]
    public Transform attackCenter;
    public float attackCooldown;
    public float damageCalTime;
    public Vector2 attackSize;
    public LayerMask enemyLayer;
    private bool isAttacking;

    private string currentState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeAnimation("Player_Idle");
    }

    private void Update()
    {
        CheckAttacking();
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
            ChangeAnimationState(h);
            ChangeRotation(h);
            isMoving = false;
        }
    }

    private void ChangeAnimationState(float h)
    {
        if (h == 0 && currentState != "Player_Jump" && currentState != "Player_Attack")
            ChangeAnimation("Player_Idle");
        else if (currentState != "Player_Jump" && currentState != "Player_Attack")
            ChangeAnimation("Player_Run");
    }

    private void ChangeRotation(float h)
    {
        if (h < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (h > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void CheckJumping()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            if(!isAttacking)
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
            isGrounded = false;
            isJumping = false;
            if(isAttacking)
                Invoke("JumpComplete", 0.5f);
        }
    }

    private void JumpComplete()
    {
        ChangeAnimation("Player_Idle");
    }

    public void JumpReset()
    {
        isGrounded = true;
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

    private void CheckAttacking()
    {
        if (!isAttacking && Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            ChangeAnimation("Player_Attack");
            Invoke("DealDamage", damageCalTime);
            Invoke("AttackComplete", attackCooldown);
        }
    }

    private void DealDamage()
    {
        RaycastHit2D hit = Physics2D.BoxCast(attackCenter.position, attackSize, 0, Vector2.zero, 0, enemyLayer);
        if (hit)
        {
            hit.collider.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }

    private void AttackComplete()
    {
        isAttacking = false;
        ChangeAnimation("Player_Idle");
    }

    private void ChangeAnimation(string newAnimation) 
    {
        if (currentState == newAnimation)
            return;
        anim.Play(newAnimation);
        currentState = newAnimation;
    }

    private void OnDrawGizmos()
    {
        if (attackCenter != null)
            Gizmos.DrawWireCube(attackCenter.position, attackSize);
    }
}
