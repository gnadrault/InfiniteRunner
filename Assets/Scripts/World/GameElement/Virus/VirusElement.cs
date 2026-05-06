using Player;
using UnityEngine;
using World.GameElement.Virus.Solution;
using World.GameElement.Virus.State;

namespace World.GameElement.Virus
{
    public abstract class VirusElement: Element
    {
        [SerializeField] protected VirusSolution solution;
        
        public abstract void ApplyEffect(PlayerController player, Transform position);
        public abstract void RemoveEffect(PlayerController player);
        
        private VirusStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new VirusStateMachine(this);
        }
        
        public override void OnPlayerCollision(PlayerController player)
        {
            _stateMachine.PlayerCollision(player);
            bool attachedVirus = player.AttachVirus(this);
            if (attachedVirus) solution.OnAttached(player);
        }

        public abstract string GetLabel();
    }
}