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

    [Header("Attack")]
    public Transform attackCenter;
    public int attackDamage;
    public float attackCooldown;
    public float damageCalTime;
    public Vector2 attackSize;
    public LayerMask enemyLayer;
    private bool isAttacking;

    [Header("Ground")]
    public Transform groundCheck;
    public float groundCheckLineSize;
    public LayerMask groundLayer;
    private bool isGrounded = true;
    private Platform lastPlatform;

    private string currentState;
    private Camera main;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        main = Camera.main;
        ChangeAnimation("Player_Idle");
        SetUpDeathTrigger();
    }

    private void SetUpDeathTrigger()
    {
        Health health = GetComponent<Health>();
        health.OnDeath += Die;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        CheckAttacking();
        CheckHorizontalMovement();
        CheckJumping();
        CheckJumpReset();
        CheckIfOutOfCamera();
    }

    private void CheckIfOutOfCamera()
    {
        if (main.transform.position.y > transform.position.y + 10.0f)
        {
            Die();
        }
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
            CheckCurrentPlatform();
            float h = Input.GetAxis("Horizontal");
            Vector2 movementVector = new Vector2(h, 0).normalized * speed;
            movementVector.y = rb.velocity.y;
            rb.velocity = movementVector;
            ChangeAnimationState(h);
            ChangeRotation(h);
            isMoving = false;
        }
    }

    private void CheckCurrentPlatform()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(groundCheck.position, new Vector3(0.5f, groundCheckLineSize, 0), 0, Vector3.zero, 0, groundLayer);
        if (hits.Length == 0 && lastPlatform != null)
        {
            lastPlatform.TryDestroy();
            lastPlatform = null;
        }
        if (hits.Length == 0)
            return;
        if (hits[0])
        {
            Platform newPlatform = hits[0].collider.gameObject.GetComponent<Platform>();
            if (newPlatform != null)
                lastPlatform = newPlatform;
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
            CheckIfTemporaryGround();
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            isGrounded = false;
            isJumping = false;
            if(isAttacking)
                Invoke("JumpComplete", 0.5f);
        }
    }

    private void CheckIfTemporaryGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(groundCheck.position, new Vector3(0.5f, groundCheckLineSize, 0), 0, Vector3.zero, 0, groundLayer);
        if (hit)
        {
            Platform newPlatform = hit.collider.GetComponent<Platform>();
            if (newPlatform != null)
                newPlatform.TryDestroy();
        }
    }

    private void JumpComplete()
    {
        ChangeAnimation("Player_Idle");
    }

    private void CheckJumpReset() 
    {
        if (isGrounded)
            return;
        RaycastHit2D hit = Physics2D.BoxCast(groundCheck.position, new Vector3(0.5f, groundCheckLineSize, 0), 0, Vector3.zero, 0, groundLayer);
        if (hit)
        {
            isGrounded = true;
        }
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
            hit.collider.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
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
        if (groundCheck != null)
            Gizmos.DrawWireCube(groundCheck.position, new Vector3(0.8f, groundCheckLineSize, 0));
    }
}
