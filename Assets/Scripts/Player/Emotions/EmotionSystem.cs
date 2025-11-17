using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player.Emotions
{
    public class EmotionSystem : MonoBehaviour
    {
        [SerializeField] private EmotionEventChannel channel;
        
        private EmotionState currentState;
        private Dictionary<Type, EmotionState> states = new();
        private Coroutine timerRoutine;

        private void Awake()
        {
            var stateTypes = typeof(EmotionState).Assembly.GetTypes();
            foreach (var t in stateTypes)
            {
                if (t.IsSubclassOf(typeof(EmotionState)) && !t.IsAbstract)
                {
                    var state = (EmotionState)Activator.CreateInstance(t, new object[] { this });
                    states[t] = state;
                }
            }

            SetState<NeutralState>();
        }

        private void OnEnable()
        {
            channel.OnEventRaised += OnEmotionTriggered;
        }

        private void OnDisable()
        {
            channel.OnEventRaised -= OnEmotionTriggered;
        }

        private void OnEmotionTriggered(EmotionType type)
        {
            switch (type)
            {
                case EmotionType.Neutral: SetState<NeutralState>(); break;
                case EmotionType.Sad: SetState<SadState>(); break;
            }
        }

        public void SetState<T>() where T : EmotionState
        {
            currentState?.Exit();
            currentState = states[typeof(T)];
            currentState.Enter();
            Debug.Log($"Emotion changed to: {typeof(T).Name}");
        }

        public EmotionState Current => currentState;

        public void StartTimer(float seconds, Action callback)
        {
            if (timerRoutine != null) StopCoroutine(timerRoutine);
            timerRoutine = StartCoroutine(Timer(seconds, callback));
        }

        private IEnumerator Timer(float t, Action cb)
        {
            yield return new WaitForSeconds(t);
            cb?.Invoke();
        }

        private void Update() => currentState.Update();
    }

}