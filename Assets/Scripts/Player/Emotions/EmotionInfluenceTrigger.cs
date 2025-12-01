using UnityEngine;

namespace Platformer.Player.Emotions
{
    public class EmotionInfluenceTrigger : MonoBehaviour
    {
        [SerializeField] private EmotionInfluenceEventChannel influenceChannel;
        [SerializeField] private EmotionInfluence influence;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            influenceChannel.RaiseEvent(influence);
        }
    }
}