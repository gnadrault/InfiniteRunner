using Effect;
using Player;
using UnityEngine;
using Utils;

namespace World.GameElement.Virus
{
    public abstract class VirusElement : Element
    {
        [SerializeField] protected float timeReduce = 0.9f;

        public abstract void ApplyEffect(PlayerController player, Transform position);
        public abstract void RemoveEffect(PlayerController player);

        public override void OnPlayerCollision(PlayerController player, Transform position)
        {
            if (player.IsPlayerInfected()) return;

            if (player.HasShield())
            {
                GameEvents.OnShieldBroken?.Invoke();
                Destroy(gameObject);
                return;
            }

            DisableVirusMovements();
            transform.SetPositionAndRotation(position.position, position.rotation);
            transform.SetParent(player.transform);
            player.AttachVirus(this);
        }

        private void DisableVirusMovements()
        {
            if (TryGetComponent(out MoveHorizontal move))
                move.enabled = false;

            Animator animator = GetComponentInChildren<Animator>();
            if (animator != null)
                animator.enabled = false;
        }
    }
}