
using Player;

namespace World.GameElement.Virus.State
{
    public class VirusStateMachine
    {
        // States
        private IVirusState _idleState;
        private IVirusState _attackState;
        private IVirusState _attachedState;
        private IVirusState _detachedState;
        private IVirusState _destroyState;
        
        private IVirusState _currentState;
        private PlayerController _playerController;
        private VirusElement _currentVirus;

        public VirusStateMachine(VirusElement currentVirus)
        {
            _idleState = new IdleState();
            _attackState = new AttackState();
            _attachedState = new AttachedState();
            _detachedState = new DetachedState();
            _destroyState = new DestroyState();
            _currentVirus = currentVirus;
            ChangeState(_idleState);
        }
        
        private void ChangeState(IVirusState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
        
        public void UpdateState()
        {
            _currentState.UpdateState();
        }

        public void PlayerCollision(PlayerController player)
        {
            _playerController = player;
            ChangeState(_attachedState);
        }
    }
}