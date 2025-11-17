using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player.Emotions
{
    public class EmotionSystem : MonoBehaviour
    {
        [SerializeField] private List<EmotionData> emotionDataList;
        [SerializeField] private EmotionEventChannel channel;
        [SerializeField] private PlayerController player;

        private EmotionState currentState;
        private Dictionary<EmotionType, EmotionState> states = new();
        private Coroutine timerRoutine;

        public PlayerController Player => player;

        private void Awake()
        {
            foreach (var data in emotionDataList)
            {
                states[data.Type] = new EmotionState(this, data);
            }

            SetNeutral();
        }

        private void OnEnable()
        {
            channel.OnEventRaised += OnEmotionTriggered;
        }

        private void OnDisable()
        {
            channel.OnEventRaised -= OnEmotionTriggered;
        }

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

            Debug.Log($"Emotion changed to {type}");
        }

        public void SetNeutral() => SetEmotion(EmotionType.Neutral);

        public void StartTimer(float seconds, Action callback)
        {
            if (timerRoutine != null) StopCoroutine(timerRoutine);
            timerRoutine = StartCoroutine(Timer(seconds, callback));
        }

        private void OnEmotionTriggered(EmotionType type)
        {
            SetEmotion(type);
        }

        //public void SetState<T>() where T : EmotionState
        //{
        //    currentState?.Exit();
        //    currentState = states[typeof(T)];
        //    currentState.Enter();
        //    Debug.Log($"Emotion changed to: {typeof(T).Name}");
        //}

        //public EmotionState Current => currentState;

        //public void StartTimer(float seconds, Action callback)
        //{
        //    if (timerRoutine != null) StopCoroutine(timerRoutine);
        //    timerRoutine = StartCoroutine(Timer(seconds, callback));
        //}

        private IEnumerator Timer(float t, Action cb)
        {
            yield return new WaitForSeconds(t);
            cb?.Invoke();
        }

        private void Update() => currentState.Update();
    }

}