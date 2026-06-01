using Data;
using Gameplay.Elements.Obstacles;
using UnityEngine;

namespace Gameplay.Segments.Spawn
{
    public class ObstacleSpawnPoint : SpawnPoint
    {
        [SerializeField] private ObstacleSize size;
        [SerializeField] private ObstacleType type;
        [SerializeField] private bool isMobile = false;
        [SerializeField] private ObstacleDatabase obstacleDatabase;

        public ObstacleSize Size => size;
        public ObstacleType Type => type;
        public bool IsMobile => isMobile;
    }
}