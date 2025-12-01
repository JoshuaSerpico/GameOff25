using UnityEngine;

namespace Platformer.Player.Emotions
{
    [CreateAssetMenu(menuName = "Emotions/Emotion Data")]
    public class EmotionData : ScriptableObject
    {
        public EmotionType Type;

        [Header("Gameplay Modifiers")]
        [Tooltip("Multiplier applied to player movement speed when this outward emotion is active. Default is 1 (no change).")]
        public float MovementSpeedModifier = 1f;

        [Tooltip("If true, player movement becomes erratic while this emotion is active.")]
        public bool IsErratic = false;
    }
}
