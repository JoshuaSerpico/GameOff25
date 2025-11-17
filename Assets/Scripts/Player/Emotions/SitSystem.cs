using UnityEngine;

namespace Platformer.Player.Emotions
{
    public class SitSystem : MonoBehaviour
    {
        [SerializeField] private EmotionEventChannel emotionEventChannel;
        [SerializeField] private EmotionType resultEmotion = EmotionType.Neutral;
        [SerializeField] private KeyCode sitKey = KeyCode.S;
        [Tooltip("Time in seconds the player must sit to calm down.")]
        [SerializeField] private float calmDuration = 2f;
        private float sitTimer = 0f;

        private void Update()
        {
            UpdateSitTimer();
        }

        private void UpdateSitTimer()
        {
            if (CanSit())
            {
                sitTimer += Time.deltaTime;
                if (sitTimer >= calmDuration)
                {
                    CalmEmotion();
                    sitTimer = 0f;
                }
            }
            else
            {
                sitTimer = 0f;
            }
        }

        private bool CanSit()
        {
            return Input.GetKey(sitKey);
        }

        private void CalmEmotion()
        {
            emotionEventChannel.RaiseEvent(resultEmotion);
        }
    }
}