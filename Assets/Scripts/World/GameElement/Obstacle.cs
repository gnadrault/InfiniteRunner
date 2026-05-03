using UnityEngine;
using World.Data;

namespace World.GameElement
{
    public class Obstacle : GameElement
    {
        [SerializeField] private ObstacleSize size;
        [SerializeField] private ObstacleType type;

        public override SpawnType SpawnType => SpawnType.Obstacle;
        public ObstacleSize Size => size;
        public ObstacleType Type => type;
    }
}
