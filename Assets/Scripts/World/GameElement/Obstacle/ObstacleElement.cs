using Data;
using Player;
using UnityEngine;

namespace World.GameElement.Obstacle
{
    [System.Serializable]
    public class ObstacleElement : Element
    {
        [SerializeField] private ObstacleSize size;
        [SerializeField] private ObstacleType type;
        [SerializeField] private bool isMobile = false;
        
        public override void OnPlayerCollision(PlayerController player, Transform position)
        {
            player.Die();
        }

        public ObstacleSize Size => size;
        public ObstacleType Type => type;
        public bool IsMobile => isMobile;
    }
}
