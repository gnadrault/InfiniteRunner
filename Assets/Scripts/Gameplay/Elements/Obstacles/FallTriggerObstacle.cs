using UnityEngine;

namespace Gameplay.Elements.Obstacles
{
    public class FallTriggerObstacle : Element
    {
        [SerializeField] private FallingObstacle obstacle;

        public override void OnPlayerCollision(Transform position)
        {
            obstacle.StartFallingObstacle();
        }
    }
}