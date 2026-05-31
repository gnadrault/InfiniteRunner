using Core;
using Gameplay.Elements;
using UnityEngine;

namespace Player
{
    public class PlayerCollision : GameBehavior
    {
        [SerializeField] private Transform attachedPlayerPosition;

        private void OnTriggerEnter(Collider other)
        {
            var element = other.GetComponentInParent<Element>();
            element?.OnPlayerCollision(attachedPlayerPosition);
        }
    }
}
