using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private BoxCollider2D checkCollider;
    [SerializeField] private LayerMask groundMask;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        IsGrounded = Physics2D.OverlapAreaAll(checkCollider.bounds.min, checkCollider.bounds.max, groundMask).Length > 0;
    }
}
