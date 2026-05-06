using Player.Data;

namespace Player.State
{
    public class DieState : IPlayerState
    {
        private readonly PlayerController _playerController;
        private readonly DieSettings _dieSettings;
        
        public DieState(PlayerController playerController, DieSettings dieSettings)
        {
            _playerController = playerController;
            _dieSettings = dieSettings;
            
            // Update death particles duration
            var main = _dieSettings.deathParticles.main;
            main.duration = _dieSettings.dieSpeed;
        }
        
        public void Enter()
        {
            _playerController.GetMeshObject().SetActive(false);
            _dieSettings.deathParticles.Play();
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