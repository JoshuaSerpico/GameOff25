using System;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyController : MonoBehaviour
{


    [Header("Movement Settings")]
    [SerializeField] private float groundSpeed = 5f;
    [SerializeField] private float drag = 0.9f;
    [SerializeField] private float groundAcceleration = 0.23f;
    [SerializeField] private float idleBufferTime = 0.1f;
    //How long until character should register as Idling; helps prevent character from swapping to idle animation between changing directions
    [SerializeField] private float idleTime = 5;
    private Rigidbody2D rb;
    private Animator animator;
    private float horizontalInput;
    [SerializeField] private float movementDecisionCooldown = 4;
    private float movementDecisionTimer;
    private float idleTimeCounter;
    private float speedModifier = 1f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GetMovementDecision();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        MoveWithInput();
        ApplyFriction();
    }


    private void UpdateAnimations()
    {
        //Animation Parameters: Moving

        if (Math.Abs(rb.linearVelocityX) < 0.6) idleTimeCounter += idleBufferTime;

        if (Math.Abs(rb.linearVelocityX) < 0.6 && idleTimeCounter > idleTime)
        {
            animator.SetBool("Moving", false);
            idleTimeCounter = 0;
        }
        else if (Math.Abs(rb.linearVelocityX) > 0.6)
        {
            animator.SetBool("Moving", true);
            idleTimeCounter = 0;
        }
    }


    // -1, 0, or 1
    private void GetMovementDecision()
    {
        movementDecisionTimer += Time.deltaTime;
        if (movementDecisionTimer >= movementDecisionCooldown)
        {
            // Prevents mob from idling for two cycles in a row
            int newDecision;
            do
            {
                newDecision = UnityEngine.Random.Range(-1, 2);
            } while (newDecision == 0 && horizontalInput == newDecision);
            horizontalInput = newDecision;

            movementDecisionTimer = 0;
        }
    }

    private void MoveWithInput()
    {
        if (Mathf.Abs(horizontalInput) > 0)
        {
            float speed = groundSpeed * speedModifier;

            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, horizontalInput * speed, groundAcceleration);

            UpdateDirection();
        }
    }


    private void UpdateDirection()
    {
        float direction = Mathf.Sign(horizontalInput);
        //Debug.Log(direction);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void ApplyFriction()
    {
        if (horizontalInput == 0)
        {
            rb.linearVelocityX *= drag;
        }
    }
}
