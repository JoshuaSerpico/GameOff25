namespace Platformer.Player.Emotions
{
    public class SadState : EmotionState
    {
        private readonly float recoveryTime = 5f;
        public SadState(EmotionSystem system) : base(system) { }

        public override float SpeedMultiplier => 0.6f;

        public override void Enter()
        {
            system.StartTimer(recoveryTime, () => system.SetState<NeutralState>());
        }
    }

}