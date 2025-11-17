using UnityEngine;

namespace Platformer.Player.Emotions
{
    [CreateAssetMenu(menuName = "Emotions/Emotion Data")]
    public class EmotionData : ScriptableObject
    {
        public EmotionType Type;
        [Tooltip("Duration of this emotion in seconds. Set to 0 for persistent states like Neutral.")]
        public float Duration = 0f;
        public float MovementSpeedModifier = 1f;
    }
}