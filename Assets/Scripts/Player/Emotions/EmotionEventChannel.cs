using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Player.Emotions
{
    [CreateAssetMenu(menuName = "Events/Emotion Event")]
    public class EmotionEventChannel : ScriptableObject
    {
        public UnityAction<EmotionType> OnEventRaised;

        public void RaiseEvent(EmotionType type)
        {
            OnEventRaised?.Invoke(type);
        }
    }
}
