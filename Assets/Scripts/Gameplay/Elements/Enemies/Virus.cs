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

        public override void OnPlayerCollision(Transform position)
        {
            if (PlayerController.Instance.IsPlayerInfected()) return;
            if (PlayerController.Instance.HasShield())
            {
                GameEvents.OnShieldBroken?.Invoke();
                Destroy(gameObject);
                return;
            }
            AttachVisually(PlayerController.Instance.transform, position);
            PlayerController.Instance.AttachVirus(this);
        }

        public void ApplyVirusEffect()
        {
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