using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player.Emotions
{
    public class EmotionSystem : MonoBehaviour
    {
        public static EmotionSystem Instance;
        
        [Header("External Emotions")]
        public string CurrentEmotion = "Neutral";
        [SerializeField] private List<EmotionData> emotionDataList;
        [SerializeField] private PlayerController player;

        [Header("Internal Emotions")]
        [SerializeField] private InternalEmotionState internalState = new InternalEmotionState();
        [SerializeField] private EmotionInfluenceEventChannel influenceChannel;

        private EmotionState currentState;
        private Dictionary<EmotionType, EmotionState> states = new();

        public PlayerController Player => player;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            foreach (var data in emotionDataList)
            {
                states[data.Type] = new EmotionState(this, data);
            }

            SetNeutral();
        }

        private void OnEnable()
        {
            if (influenceChannel != null)
                influenceChannel.OnEventRaised += ApplyInfluence;
        }

        private void OnDisable()
        {
            if (influenceChannel != null)
                influenceChannel.OnEventRaised -= ApplyInfluence;
        }

        private void Update()
        {
            // Update internal emotion axes (decay/normalization)
            internalState.Update(Time.deltaTime);

            // Map internal emotions → outward emotion type
            UpdateOutwardEmotion();

            // Update the current external state
            currentState?.Update();
        }

        #region External Emotion Methods
        public void SetEmotion(EmotionType type)
        {
            if (!states.ContainsKey(type))
            {
                Debug.LogWarning($"Emotion {type} not found!");
                return;
            }

            currentState?.Exit();
            currentState = states[type];
            currentState.Enter();

            CurrentEmotion = type.ToString();

            Debug.Log($"Outward emotion changed to {type}");
        }

        public void SetNeutral() => SetEmotion(EmotionType.Neutral);

        #endregion

        #region Internal Emotion Methods
        private void UpdateOutwardEmotion()
        {
            // Example mapping rules (tune these later)
            EmotionType newEmotion = EmotionType.Neutral;

            if (internalState.Valence < 0.35f && internalState.Arousal > 0.65f && internalState.Control > 0.5f)
                newEmotion = EmotionType.Angry;

            else if (internalState.Valence < 0.4f && internalState.Arousal < 0.4f)
                newEmotion = EmotionType.Sad;

            else if (internalState.Valence > 0.7f && internalState.Arousal > 0.7f)
                newEmotion = EmotionType.Excited;

            // Only change external state if it’s different
            if (currentState == null || currentState != states[newEmotion])
            {
                SetEmotion(newEmotion);
            }
        }
        #endregion

        #region Public Influence Method
        public void ApplyInfluence(EmotionInfluence influence)
        {
            internalState.ApplyInfluence(influence);
        }
        #endregion
    }
}