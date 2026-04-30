using UnityEngine;

namespace Player.State
{
    public class IdleState : MonoBehaviour, IPlayerState
    {
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
