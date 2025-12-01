using UnityEngine;

namespace Platformer.Player.Emotions
{
    public class FallDetector : MonoBehaviour
    {
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private EmotionSystem emotionSystem;
        [SerializeField] private float emotionFallThreshold = 3f;

        private bool IsGrounded => groundChecker.IsGrounded;
        private bool wasGrounded = true;
        private float fallStartY;

        private void Update()
        {
            MeasureFallStart();
            HandleLanding();

            wasGrounded = IsGrounded;
        }

        private void MeasureFallStart()
        {
            if (wasGrounded && !IsGrounded)
            {
                fallStartY = transform.position.y;
            }
        }

        private void HandleLanding()
        {
            if (!wasGrounded && IsGrounded)
            {
                float fallDistance = fallStartY - transform.position.y;

                if (fallDistance >= emotionFallThreshold)
                {
                    EmotionInfluence influence = new EmotionInfluence
                    {
                        dValence = -0.3f,  // falls make the player sad/angry
                        dArousal = +0.5f,
                        dControl = -0.2f,
                        dConnection = 0f
                    };
                    if (emotionSystem != null)
                        emotionSystem.ApplyInfluence(influence);
                }
            }
        }
    }
}