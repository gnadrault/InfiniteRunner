namespace Backup.StateMachine.State
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine stateMachine, PlayerController controller) : base(stateMachine, controller)
        {
            
        }

        protected override void CheckTransitions()
        {
            // Priority: Jump > Move
            if (Controller.IsJumpRequested)
            {
                StateMachine.ChangeState(Controller.JumpingState);
            }
            else if (Controller.IsMoveRequested)
            {
                // Update anchor index and transition
                //Controller.SetCurrentAnchorIndex(Controller.TargetAnchorIndex);
                StateMachine.ChangeState(Controller.MovingState);
            }
        }
    }
}