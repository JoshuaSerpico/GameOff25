namespace Platformer.Player.Emotions
{
    public class EmotionState
    {
        protected EmotionSystem emotionSystem;
        protected EmotionData data;

        public EmotionState(EmotionSystem system, EmotionData data)
        {
            this.emotionSystem = system;
            this.data = data;
        }

        public virtual void Enter()
        {
            ApplyEffects();
        }

        public virtual void Exit()
        {
            emotionSystem.Player.ResetMovementModifier();
            emotionSystem.Player.ResetErraticMovement();
        }

        public virtual void Update() { }

        protected void ApplyEffects()
        {
            emotionSystem.Player.SetMovementModifier(data.MovementSpeedModifier);

            if (data.IsErratic)
            {
                emotionSystem.Player.EnableErraticMovement();
            }
        }
    }
}