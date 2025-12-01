using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Collider2D checkCollider;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float reduceColliderCheckArea;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        IsGrounded = Physics2D.OverlapBox(
            checkCollider.bounds.center,
            checkCollider.bounds.size - new Vector3(0, reduceColliderCheckArea, 0),
            0,
            groundMask
        );
    }
}
