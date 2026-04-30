namespace Backup.StateMachine.State
{
    public abstract class PlayerBaseState
    {
        protected PlayerStateMachine StateMachine { get; }
        protected PlayerController Controller { get; }
        
        protected bool IsExitingState { get; private set; }

        protected PlayerBaseState(PlayerStateMachine playerStateMachine, PlayerController playerController)
        {
            StateMachine = playerStateMachine;
            Controller = playerController;
        }

        public virtual void Enter()
        {
            IsExitingState  = false;
        }

        public virtual void Update()
        {
            CheckTransitions();
            Controller.ConsumeInputFlags();
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