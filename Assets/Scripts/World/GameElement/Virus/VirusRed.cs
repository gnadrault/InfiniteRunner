using Effect;
using Player;
using UnityEngine;

namespace World.GameElement.Virus
{
    public class VirusRed: VirusElement
    {
        
        public override void ApplyEffect(PlayerController player, Transform position)
        {
            player.DisableMovement();
        }

        public override void RemoveEffect(PlayerController player)
        {
            player.EnableMovement();
            Destroy(gameObject);
        }

        public override void OnPlayerCollision(PlayerController player, Transform position)
        {
            DisableMovement();
            base.OnPlayerCollision(player, position);
        }

        private void DisableMovement()
        {
            GetComponent<MoveHorizontal>().enabled = false;
            GetComponentInChildren<Animator>().enabled = false;
        }

        public override string GetLabel()
        {
           return solution.GetLabel();
        }
    }
}