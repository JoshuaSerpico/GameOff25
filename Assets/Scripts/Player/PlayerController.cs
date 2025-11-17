using Platformer.Player.Emotions;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string JumpButton = "Jump";
    private const string HorizontalAxis = "Horizontal";

    [SerializeField] private EmotionSystem emotionSystem;

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
    [SerializeField] private float jitterIntensity = 0.2f;

    [Header("Ground")]
    [SerializeField] private GroundChecker groundChecker;
    private bool IsGrounded => groundChecker.IsGrounded;

    private Rigidbody2D rb;
    private float horizontalInput;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private float speedModifier = 1f;
    private bool isErratic = false;

    public void SetMovementModifier(float modifier) => speedModifier = modifier;
    public void ResetMovementModifier() => speedModifier = 1f;
    public void EnableErraticMovement() => isErratic = true;
    public void ResetErraticMovement() => isErratic = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInput();
        UpdateJumpBuffer();
        UpdateCoyoteTime();
        HandleJump();
    }

    private void FixedUpdate()
    {
        MoveWithInput();
        ApplyFriction();
        ApplyErraticMovement();
    }

    private void ApplyErraticMovement()
    {
        if (isErratic)
        {
            rb.linearVelocityX += UnityEngine.Random.Range(-jitterIntensity, jitterIntensity);
        }
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
        if (IsGrounded)
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
            float appliedAcceleration = IsGrounded ? groundAcceleration : airAcceleration;

            float speed = groundSpeed * speedModifier;

            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, horizontalInput * speed, appliedAcceleration);

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

    private void ApplyFriction()
    {
        if (IsGrounded && horizontalInput == 0 && rb.linearVelocityY <= 0)
        {
            rb.linearVelocity *= drag;
        }
    }
}
