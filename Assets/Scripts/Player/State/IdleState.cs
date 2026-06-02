using Player.Data;

namespace Player.State
{
    /// <summary>
    /// Manage the player idle state
    /// </summary>
    public class IdleState : IPlayerState
    {
        private readonly IdleSettings _idleSettings;

        public void Enter()
        {
        }

        public void UpdateState()
        {
        }

        public void Exit()
        {
        }

        public bool IsDone()
        {
            return false;
        }
    }
}
