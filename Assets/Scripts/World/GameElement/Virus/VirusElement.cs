using Player;
using UnityEngine;
using World.GameElement.Virus.Solution;

namespace World.GameElement.Virus
{
    public abstract class VirusElement: Element
    {
        [SerializeField] protected VirusSolution solution;
        
        public abstract void ApplyEffect(PlayerController player, Transform position);
        public abstract void RemoveEffect(PlayerController player);
        
        public override void OnPlayerCollision(PlayerController player, Transform position)
        {
            transform.SetPositionAndRotation(position.position, position.rotation);
            transform.SetParent(player.transform);
            bool attachedVirus = player.AttachVirus(this);
            if (attachedVirus) solution.OnAttached(player);
        }

        public abstract string GetLabel();
    }
}