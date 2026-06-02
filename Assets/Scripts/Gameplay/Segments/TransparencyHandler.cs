using Core;
using Gameplay.Elements.Obstacles;
using UnityEngine;

namespace Gameplay.Segments
{
    /// <summary>
    /// Script to make obstacles transparent on collision with the current game object
    /// </summary>
    public class TransparencyHandler : GameBehavior
    {
        private void OnTriggerEnter(Collider other)
        {
            var obstacle = other.GetComponentInParent<Obstacle>();
            obstacle?.OnTransparencyCollision();
        }
    }
}
