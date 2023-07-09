using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlatformerInput platformerInputActions;
    private InputAction Movement;


    [SerializeField] private float deadZone = -30;

    ////Camera
    //[Header("Camera Target")]
    //[SerializeField] private Transform cameraTarget;
    //[SerializeField] private float aheadAmount;
    //[SerializeField] private float aheadTime;
    //[Space]

    [SerializeField] private LayerMask groundLayer;
    
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private Animator animator;
    private float movement;
    public bool isPushing=false;

    [SerializeField] private bool onGround;
    private void Awake()
    {
        platformerInputActions = new PlatformerInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Movement = platformerInputActions.Player.Movement;
        Movement.Enable();

        platformerInputActions.Player.Jump.performed += DoJump;
        platformerInputActions.Player.Jump.Enable();

        //platformerInputActions.Player.Dash.performed += DoDash;
        //platformerInputActions.Player.Dash.Enable();
    }
    private void OnDisable()
    {
        Movement.Disable();
        platformerInputActions.Player.Jump.Disable();
        //platformerInputActions.Player.Dash.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        #region TIMERS
        lastGroundedTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;
        #endregion
        Gravity();
        movement = GetInput().x;

        animator.SetFloat("movement", Mathf.Abs(movement));

        // Jump
        if (lastJumpTime >= 0f && lastGroundedTime > 0f)
            Jump();
        
        // Knockback
        if (knockbacked)
        {
            Vector2 dir = isFacingRight ? Vector2.left : Vector2.right;
            rb.AddForce(dir*test);
        }

        if (isDashing)
        {
            rb.velocity = dashingDirection.normalized*dashingVelocity;
            return;
        }

        animator.SetBool("isPushing", isPushing);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            FindObjectOfType<LevelManager>().LoadLevelById(0);
        }
    }


    private void FixedUpdate()
    {
        Collisions(); 
        Run();
    }
    #region Run
    [Header("Run")]
    [SerializeField] private float maxMoveSpeed=10f;
    [SerializeField] private float acceleration=90f;
    [SerializeField] private float deceleration = 60f;
    [SerializeField] private float vel = 0.9f;
    private void Run()
    {
        if (knockbacked)
            return;
        float speed = movement * maxMoveSpeed;
        float dif = speed - rb.velocity.x;
        float acc = (Mathf.Abs(speed) > 0.0f) ? acceleration : deceleration;
        float target = Mathf.Pow(Mathf.Abs(dif) * acc, vel) * Mathf.Sign(dif);

        rb.AddForce(target * Vector2.right);

        if (movement > 0 && !isFacingRight) FlipDirection();
        if (movement < 0 && isFacingRight) FlipDirection();
    }
    private void FlipDirection()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight;
    }
    #endregion
    #region Jump
    [Header("Jump")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float jumpCoyoteTime;
    private float lastGroundedTime;
    [SerializeField] private float jumpBufferTime;
    private float lastJumpTime;
    private void DoJump(InputAction.CallbackContext obj)
    {
        lastJumpTime = jumpBufferTime;
    }
    private void Jump()
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        //particle.SetActive(true);
        //particle.transform.position = particleTarget.position;
        //particle.GetComponent<Particle>().PlayParticle();

        animator.SetBool("isJumping", true);
    }
    #endregion
    #region DASHING
    [Header("Dash")]
    [SerializeField] private float dashingVelocity = 14f;
    [SerializeField] private float dashingTime = 0.7f;
    private Vector2 dashingDirection;
    private bool isDashing;
    private bool canDash=true;
    private void DoDash(InputAction.CallbackContext obj)
    {
        if (!canDash)
            return;

        isDashing = true;
        canDash = false;
        dashingDirection = GetInput();
        if (dashingDirection == Vector2.zero)
        {
            dashingDirection = new Vector2(transform.localScale.x, 0);
        }
        StartCoroutine(StopDashing());
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
    }
    #endregion
    #region KNOCKBACK
    [Header("Knockback")]
    [SerializeField] private float knockbackVelocity = 1f;
    [SerializeField] private float knockbackTime = 1f;
    [SerializeField] private float test;
    private bool knockbacked=false;
    public void Knockback(Transform target)
    {
        Vector2 direction = transform.position - target.position;
        knockbacked = true;
        animator.SetBool("isFade", true);
        rb.velocity = direction.normalized * knockbackVelocity;
        StartCoroutine(Unknockback());
    }
    private IEnumerator Unknockback()
    {
        yield return new WaitForSeconds(knockbackTime);
        knockbacked = false;
        animator.SetBool("isFade", false);
    }
    #endregion

    private void Gravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
            animator.SetBool("isFalling", true);
        }
        else if(rb.velocity.y > 0 /*&& !Input.GetButton("Jump")*/)
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;

        if (transform.position.y <= deadZone)
        {
            gameObject.GetComponent<PlayerHealth>().Kill();
        }
    }
    [SerializeField] private float groundDis;
    private void Collisions()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundDis, groundLayer);
        animator.SetBool("isGrounded", onGround);
        if (onGround)
        {
            lastGroundedTime = jumpCoyoteTime;
            canDash = true;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
            
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundDis);
    }
    private Vector2 GetInput()
    {
        return Movement.ReadValue<Vector2>();
    }
}
