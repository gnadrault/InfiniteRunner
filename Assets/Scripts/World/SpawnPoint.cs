using UnityEngine;

namespace World
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private SpawnType spawnType;
        [SerializeField] private ObstacleSize maxSize;
        [SerializeField] private float probability;
    }
}
