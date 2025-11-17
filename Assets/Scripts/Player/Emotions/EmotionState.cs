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

            if (data.Duration > 0)
            {
                emotionSystem.StartTimer(data.Duration, () => emotionSystem.SetNeutral());
            }
        }

        public virtual void Exit()
        {
            emotionSystem.Player.ResetMovementModifier();
        }

        public virtual void Update() { }

        protected void ApplyEffects()
        {
            emotionSystem.Player.SetMovementModifier(data.MovementSpeedModifier);
        }
    }
}