using Player.Data;

namespace Player.State
{
    public class DieState : IPlayerState
    {
        private readonly DieSettings _dieSettings;
        
        public DieState(DieSettings dieSettings)
        {
            _dieSettings = dieSettings;
            
            // Update death particles duration
            var main = _dieSettings.deathParticles.main;
            main.duration = _dieSettings.dieSpeed;
        }
        
        public void Enter()
        {
            PlayerController.Instance.GetMeshObject().SetActive(false);
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
            return _dieSettings.deathParticles.isStopped;
        }
    }
}