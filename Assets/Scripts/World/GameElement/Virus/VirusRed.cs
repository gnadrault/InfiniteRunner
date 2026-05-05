using Player;
using UnityEngine;

namespace World.GameElement.Virus
{
    public class VirusRed: VirusElement
    {
        public override void ApplyEffect(PlayerController player, Transform position)
        {
            player.DisableMovement();
            transform.SetPositionAndRotation(position.position, position.rotation);
            transform.SetParent(player.transform);
        }

        public override void RemoveEffect(PlayerController player)
        {
            player.EnableMovement();
            Destroy(gameObject);
        }

        public override string GetLabel()
        {
           return solution.GetLabel();
        }
    }
}