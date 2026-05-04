using UnityEngine;
using World.GameElement;

namespace Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
    
        private void OnTriggerEnter(Collider other)
        {
            var element = other.GetComponentInParent<Element>();
            element?.OnPlayerCollision(playerController);
        }
    }
}
