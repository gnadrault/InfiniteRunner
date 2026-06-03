using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Obstacles
{
    /// <summary>
    /// Obstacle game object (mobile, fixed)
    /// </summary>
    public class Obstacle : Element
    {
        public override void OnPlayerCollision(Transform position)
        {
            PlayerController.Instance.Die();
        }
    }
}
