using UnityEngine;

namespace Platformer.Player.Emotions
{
    public class EmotionTrigger : MonoBehaviour
    {
        [SerializeField] private EmotionType emotionType;
        [SerializeField] private EmotionEventChannel channel;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            channel.RaiseEvent(emotionType);
        }
    }
}