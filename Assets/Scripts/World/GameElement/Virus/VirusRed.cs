using Effect;
using Player;
using UnityEngine;
using Utils;

namespace World.GameElement.Virus
{
    public class VirusRed: VirusElement
    {
        public override void ApplyEffect(PlayerController player, Transform position)
        {
            HUD.Instance.ShowVirusPanel(solution.GetLabel());
            player.DisableMovement();
            TimeScaleManager.Instance.SetTimeScale(timeReduce);
        }

        public override void RemoveEffect(PlayerController player)
        {
            player.EnableMovement();
            HUD.Instance.HideVirusPanel();
            TimeScaleManager.Instance.SetTimeScale(1f);
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