using Effect;
using UnityEngine;

namespace World.GameElement.Virus.State
{
    public class AttachedState : IVirusState
    {
        
        /*AttachedState
        ├── Enter()  → colle au joueur, applique effet, active input
        ├── Update() → vérifie solution ou timeout
        └── Exit()   → retire effet, désabonne input*/
        
        private VirusElement _virus;

        public AttachedState(VirusElement virus)
        {
            _virus = virus;
        }
        
        public void Enter()
        {
            _virus.GetComponent<MoveHorizontal>().enabled = false;
            _virus.GetComponentInChildren<Animator>().enabled = false;
        }

        public void UpdateState()
        {
            
        }

        public void Exit()
        {
        }
    }
}