using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D checkCollider;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float reduceColliderCheckArea;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        IsGrounded = Physics2D.OverlapAreaAll(checkCollider.bounds.min, checkCollider.bounds.max - new Vector3(0, reduceColliderCheckArea, 0), groundMask).Length > 0;
    }
}
