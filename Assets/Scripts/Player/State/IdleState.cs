using Player.Data;

namespace Player.State
{
    public class IdleState : IPlayerState
    {
        private readonly PlayerController _playerController;
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
