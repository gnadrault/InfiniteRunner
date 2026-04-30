namespace Backup.StateMachine.State
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine stateMachine, PlayerControllerSm controllerSm) : base(stateMachine, controllerSm)
        {
            
        }

        protected override void CheckTransitions()
        {
            // Priority: Jump > Move
            if (ControllerSm.IsJumpRequested)
            {
                StateMachine.ChangeState(ControllerSm.JumpingState);
            }
            else if (ControllerSm.IsMoveRequested)
            {
                // Update anchor index and transition
                //Controller.SetCurrentAnchorIndex(Controller.TargetAnchorIndex);
                StateMachine.ChangeState(ControllerSm.MovingState);
            }
        }
    }
}