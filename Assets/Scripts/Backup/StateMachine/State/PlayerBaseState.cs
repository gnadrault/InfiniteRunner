namespace Backup.StateMachine.State
{
    public abstract class PlayerBaseState
    {
        protected PlayerStateMachine StateMachine { get; }
        protected PlayerControllerSm ControllerSm { get; }
        
        protected bool IsExitingState { get; private set; }

        protected PlayerBaseState(PlayerStateMachine playerStateMachine, PlayerControllerSm playerControllerSm)
        {
            StateMachine = playerStateMachine;
            ControllerSm = playerControllerSm;
        }

        public virtual void Enter()
        {
            IsExitingState  = false;
        }

        public virtual void Update()
        {
            CheckTransitions();
            ControllerSm.ConsumeInputFlags();
        }

        public virtual void Exit()
        {
            IsExitingState = true;
        }
        
        /// <summary>
        /// Check if state should transition to another state
        /// </summary>
        protected abstract void CheckTransitions();
    }
}