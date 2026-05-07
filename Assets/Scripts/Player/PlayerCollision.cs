using UnityEngine;
using World.GameElement;

namespace Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private Transform attachedPlayerPosition;
        
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponentInParent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var element = other.GetComponentInParent<Element>();
            element?.OnPlayerCollision(_playerController, attachedPlayerPosition);
        }
    }
}
