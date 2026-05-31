using Core;
using Gameplay.Elements.Obstacles;
using UnityEngine;

namespace Gameplay.Segments
{
    public class TransparencyHandler : GameBehavior
    {
        private void OnTriggerEnter(Collider other)
        {
            var obstacle = other.GetComponentInParent<Obstacle>();
            obstacle?.OnTransparencyCollision();
        }
    }
}
