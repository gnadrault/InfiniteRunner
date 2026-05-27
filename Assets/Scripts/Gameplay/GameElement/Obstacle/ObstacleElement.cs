using Data;
using Player;
using UnityEngine;
using Utils;

namespace Gameplay.GameElement.Obstacle
{
    [System.Serializable]
    public class ObstacleElement : Element
    {
        [SerializeField] private ObstacleSize size;
        [SerializeField] private ObstacleType type;
        [SerializeField] private bool isMobile;
        
        public override void OnPlayerCollision(PlayerController player, Transform position)
        {
            if (player.HasGhost())
                GameEvents.OnGhostBroken?.Invoke();
            else
                player.Die();
        }

        public ObstacleSize Size => size;
        public ObstacleType Type => type;
        public bool IsMobile => isMobile;
    }
}
