using Data;
using UnityEngine;
using World.GameElement.Obstacle;

namespace World.Spawn
{
    public class ObstacleSpawnPoint : SpawnPoint
    {
        [SerializeField] private ObstacleSize size;
        [SerializeField] private ObstacleType type;
        [SerializeField] private bool isMobile = false;

        public ObstacleSize Size => size;
        public ObstacleType Type => type;
        public bool IsMobile => isMobile;
    }
}