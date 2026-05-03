using Player;
using UnityEngine;
using World.Spawn;

namespace World.GameElement
{
    public class Obstacle : GameElement
    {
        [SerializeField] private ObstacleSize size;
        [SerializeField] private ObstacleType type;

        public override SpawnType SpawnType => SpawnType.Obstacle;
        public override void OnPlayerCollision(PlayerController player)
        {
            player.Die();
        }

        public ObstacleSize Size => size;
        public ObstacleType Type => type;
    }
}
