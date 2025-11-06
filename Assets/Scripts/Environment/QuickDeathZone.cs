using UnityEngine;

// A death zone trigger that resets the player's position upon contact

public class QuickDeathZone : MonoBehaviour
{
    [Header("Death Zone Settings")]
    [Tooltip("Position to reset the player to upon death")]
    [SerializeField] private Vector2 resetPosition = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset player's position
            other.transform.position = resetPosition;
        }
    }
}
