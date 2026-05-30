using Gameplay.Elements.Collectibles;
using UnityEngine;

namespace Player
{
    public class PlayerMagnetZone : MonoBehaviour
    {
        [SerializeField] private float magnetForce = 50f;

        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponentInParent<PlayerController>();
        }

        /// <summary>
        /// Activate magnet on objet inside beam to the target point
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            Letter letter = other.GetComponentInParent<Letter>();
            if (letter != null)
            {
                letter.ActivateMagnet(_playerController.transform, magnetForce);
            }
        }
    }
}