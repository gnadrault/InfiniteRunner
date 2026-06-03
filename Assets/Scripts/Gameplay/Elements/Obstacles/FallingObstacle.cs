using UnityEngine;
using Utils;

namespace Gameplay.Elements.Obstacles
{
    /// <summary>
    /// Falling obstacle game object
    /// </summary>
    public class FallingObstacle : Obstacle
    {
        [Header("Properties")]
        [SerializeField] private Transform meshPosition;
        [SerializeField] private Transform endPosition;
        [SerializeField] private float fallingDuration = 0.5f;

        public void StartFallingObstacle()
        {
            Vector3 initPosition = meshPosition.localPosition;
            Vector3 targetPosition = endPosition.localPosition;
            StartCoroutine(TweenUtils.Transition(t =>
                    meshPosition.localPosition = Vector3.Lerp(initPosition, targetPosition, TweenUtils.EaseInQuad(t))
                , fallingDuration));
        }
    }
}