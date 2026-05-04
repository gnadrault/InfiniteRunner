using UnityEngine;
using World.GameElement.Obstacle;

namespace World.Spawn
{
    public class ObstacleSpawnPoint : SpawnPoint
    {
        [SerializeField] private ObstacleElement prefab;
        
        public ObstacleElement Spawn(Transform parent)
        {
            return Instantiate(prefab, transform.position, Quaternion.identity, parent);
        }
    }
}