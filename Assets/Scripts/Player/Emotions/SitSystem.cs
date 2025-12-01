using UnityEngine;

namespace Platformer.Player.Emotions
{
    public class SitSystem : MonoBehaviour
    {
        [SerializeField] private EmotionSystem emotionSystem;
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
            EmotionInfluence calmInfluence = new EmotionInfluence
            {
                dValence = 0.1f,
                dArousal = -0.3f,
                dControl = +0.05f,
                dConnection = 0f
            };

            if (emotionSystem != null)
                emotionSystem.ApplyInfluence(calmInfluence);
        }
    }
}