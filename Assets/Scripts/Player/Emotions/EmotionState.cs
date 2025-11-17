using UnityEngine;

namespace Platformer.Player.Emotions
{
    public abstract class EmotionState
    {
        protected EmotionSystem system;

        public EmotionState(EmotionSystem system) => this.system = system;

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }

        public virtual float SpeedMultiplier => 1f;
        public virtual Vector3 MovementOffset(Vector3 input) => input;
    }
}