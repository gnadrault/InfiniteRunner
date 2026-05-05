using Player;
using UnityEngine;
using World.GameElement.Virus.Solution;

namespace World.GameElement.Virus
{
    public abstract class VirusElement: Element
    {
        //[SerializeField] private VirusMovement movement;
        [SerializeField] protected VirusSolution solution;
        
        public abstract void ApplyEffect(PlayerController player, Transform position);
        public abstract void RemoveEffect(PlayerController player);
        
        public override void OnPlayerCollision(PlayerController player)
        {
            bool attachedVirus = player.AttachVirus(this);
            if (attachedVirus) solution.OnAttached(player);
        }

        public abstract string GetLabel();
    }
}