using Data;
using Movement;
using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Enemies
{
    /// <summary>
    /// Abstract class for virus
    /// </summary>
    public abstract class Virus : Element
    {
        [Header("Database")]
        [SerializeField] protected GameFeelProfile gameFeelProfile;
        
        [Header("Settings")]
        [SerializeField] protected string alertTitleText = "INFECTED!";

        /// <summary>
        /// Manage the player collision with an virus
        /// </summary>
        /// <param name="position"></param>
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

        /// <summary>
        /// Apply the virus effect
        /// </summary>
        public void ApplyVirusEffect()
        {
            OnApply();
        }
        
        /// <summary>
        /// Remove the virus effect
        /// </summary>
        public void RemoveVirusEffect()
        {
            OnRemove();
        }

        /// <summary>
        /// Set the virus to the player position visually and disable animations, scripts
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="position"></param>
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