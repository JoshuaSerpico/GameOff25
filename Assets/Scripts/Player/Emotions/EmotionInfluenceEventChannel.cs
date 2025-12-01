using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Player.Emotions
{
    [CreateAssetMenu(menuName = "Events/Emotion Influence Event")]
    public class EmotionInfluenceEventChannel : ScriptableObject
    {
        public UnityAction<EmotionInfluence> OnEventRaised;

        public void RaiseEvent(EmotionInfluence influence)
        {
            OnEventRaised?.Invoke(influence);
        }
    }
}
