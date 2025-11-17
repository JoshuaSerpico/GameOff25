using UnityEngine;

namespace Platformer.Player.Emotions
{
    public class FallDetector : MonoBehaviour
    {
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private EmotionEventChannel emotionChannel;
        [SerializeField] private EmotionType emotionType = EmotionType.Angry;
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
                    emotionChannel.RaiseEvent(emotionType);
                }
            }
        }
    }
}