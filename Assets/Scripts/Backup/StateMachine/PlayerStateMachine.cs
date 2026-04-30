using Backup.StateMachine.State;

namespace Backup.StateMachine
{
    public class PlayerStateMachine
    {
        public PlayerBaseState CurrentState { get; private set; }

        public void Initialize(PlayerBaseState initState)
        {
            CurrentState = initState;
            CurrentState.Enter();
        }

        public void ChangeState(PlayerBaseState newPlayerState)
        {
            CurrentState.Exit();
            CurrentState = newPlayerState;
            CurrentState.Enter();
        }

        public void UpdateState()
        {
            CurrentState.Update();
        }
    }
}