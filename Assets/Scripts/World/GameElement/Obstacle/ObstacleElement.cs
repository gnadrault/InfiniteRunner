using Player;
using UnityEngine;

namespace World.GameElement.Obstacle
{
    [System.Serializable]
    public class ObstacleElement : Element
    {
        [SerializeField] private ObstacleSize size;
        [SerializeField] private ObstacleType type;
        
        public override void OnPlayerCollision(PlayerController player)
        {
            player.Die();
        }

        public ObstacleSize Size => size;
        public ObstacleType Type => type;
    }
}
