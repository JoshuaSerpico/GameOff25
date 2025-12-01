using UnityEngine;

namespace Platformer.Player.Emotions
{
    [System.Serializable]
    public class InternalEmotionState
    {
        [Header("Internal Emotions")]
        [Range(0f, 1f)] public float Valence = 0.5f;
        [Range(0f, 1f)] public float Arousal = 0.5f;
        [Range(0f, 1f)] public float Control = 0.5f;
        [Range(0f, 1f)] public float Connection = 0.5f;

        [Header("Decay Rates")]
        [Tooltip("Rate at which Valence decays towards neutral (0.5) per second")]
        public float ValenceDecayRate = 0.01f;
        [Tooltip("Rate at which Arousal decays towards neutral (0.5) per second")]
        public float ArousalDecayRate = 0.01f;
        [Tooltip("Rate at which Control decays towards neutral (0.5) per second")]
        public float ControlDecayRate = 0.01f;
        [Tooltip("Rate at which Connection decays towards neutral (0.5) per second")]
        public float ConnectionDecayRate = 0.01f;

        private const float baseline = 0.5f;

        public void Update(float dt)
        {
            Valence = Mathf.MoveTowards(Valence, baseline, ValenceDecayRate * dt);
            Arousal = Mathf.MoveTowards(Arousal, baseline, ArousalDecayRate * dt);
            Control = Mathf.MoveTowards(Control, baseline, ControlDecayRate * dt);
            Connection = Mathf.MoveTowards(Connection, baseline, ConnectionDecayRate * dt);
        }

        public void ApplyInfluence(EmotionInfluence influence)
        {
            Valence = Mathf.Clamp01(Valence + influence.dValence);
            Arousal = Mathf.Clamp01(Arousal + influence.dArousal);
            Control = Mathf.Clamp01(Control + influence.dControl);
            Connection = Mathf.Clamp01(Connection + influence.dConnection);
        }
    }
}