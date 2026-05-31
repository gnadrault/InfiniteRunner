using Data;
using Movement;
using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Enemies
{
    public abstract class Virus : Element
    {
        [SerializeField] protected GameFeelProfile gameFeelProfile;
        [SerializeField] protected string alertTitleText = "INFECTED!";
        protected PlayerController player;

        public override void OnPlayerCollision(PlayerController playerController, Transform position)
        {
            if (playerController.IsPlayerInfected()) return;
            if (playerController.HasShield())
            {
                GameEvents.OnShieldBroken?.Invoke();
                Destroy(gameObject);
                return;
            }
            AttachVisually(playerController.transform, position);
            playerController.AttachVirus(this);
        }

        public void ApplyVirusEffect(PlayerController playerController)
        {
            player = playerController;
            OnApply();
        }
        
        public void RemoveVirusEffect()
        {
            OnRemove();
        }

        private void AttachVisually(Transform parent, Transform position)
        {
            if (TryGetComponent(out MoveHorizontal move))
                move.enabled = false;

            Animator animator = GetComponentInChildren<Animator>();
            if (animator != null)
                animator.enabled = false;
            
            transform.SetPositionAndRotation(position.position, position.rotation);
            transform.SetParent(parent);
        }
        
        protected abstract void OnApply();
        protected abstract void OnRemove();
    }
}