using Core;
using Gameplay.Elements;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Manage the player collisions
    /// </summary>
    public class PlayerCollision : GameBehavior
    {
        [Header("Settings")]
        [SerializeField] private Transform attachedPlayerPosition;

        private void OnTriggerEnter(Collider other)
        {
            var element = other.GetComponentInParent<Element>();
            element?.OnPlayerCollision(attachedPlayerPosition);
        }
    }
}
