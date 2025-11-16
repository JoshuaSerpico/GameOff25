using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string JumpButton = "Jump";
    private const string HorizontalAxis = "Horizontal";

    [Header("Movement Settings")]
    [SerializeField] private float groundSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [Tooltip("Reduces upward velocity when releasing the jump.")]
    [SerializeField] private float jumpHoldMultiplier = 0.5f;
    [Range(0f, 1f)]
    [SerializeField] private float drag = 0.9f;
    [SerializeField] private float groundAcceleration = 0.23f;
    [SerializeField] private float airAcceleration = 0.1f;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.1f;

    [Header("Ground")]
    [SerializeField] private BoxCollider2D groundCheck;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInput();
        UpdateJumpBuffer();
        CheckGround();
        UpdateCoyoteTime();
        HandleJump();
    }

    private void FixedUpdate()
    {
        MoveWithInput();
        ApplyFriction();
    }

    private void UpdateJumpBuffer()
    {
        if (Input.GetButtonDown(JumpButton))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void UpdateCoyoteTime()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw(HorizontalAxis);
    }

    private void MoveWithInput()
    {
        if (Mathf.Abs(horizontalInput) > 0)
        {
            float appliedAcceleration = isGrounded ? groundAcceleration : airAcceleration;

            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, horizontalInput * groundSpeed, appliedAcceleration);

            UpdateDirection();
        }
    }

    private void HandleJump()
    {
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            rb.linearVelocityY = jumpSpeed;
            coyoteTimeCounter = 0;
            jumpBufferCounter = 0;
        }

        if (Input.GetButtonUp(JumpButton) && rb.linearVelocityY > 0)
        {
            rb.linearVelocityY = rb.linearVelocityY * jumpHoldMultiplier;
        }
    }

    private void UpdateDirection()
    {
        float direction = Mathf.Sign(horizontalInput);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void CheckGround()
    {    
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    private void ApplyFriction()
    {
        if (isGrounded && horizontalInput == 0 && rb.linearVelocityY <= 0)
        {
            rb.linearVelocity *= drag;
        }
    }
}
