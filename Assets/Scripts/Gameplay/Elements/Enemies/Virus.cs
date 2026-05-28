using Feedback.Data;
using Movement;
using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Enemies
{
    public abstract class Virus : Element
    {
        [SerializeField] protected GameFeelProfile gameFeelProfile;
        
        protected readonly string textMessage = "INFECTED!";

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