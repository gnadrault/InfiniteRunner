using Core;
using Gameplay.Elements.Collectibles;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Manage the player magnet area collision with other objects
    /// </summary>
    public class PlayerMagnetZone : GameBehavior
    {
        [Header("Settings")]
        [SerializeField] private float magnetForce = 50f;

        /// <summary>
        /// Activate magnet on objet inside beam to the target point
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            Collectible collectible = other.GetComponentInParent<Collectible>();
            if (collectible != null)
            {
                collectible.ActivateMagnet(PlayerController.Instance.GetTransform(), magnetForce);
            }
        }
    }
}