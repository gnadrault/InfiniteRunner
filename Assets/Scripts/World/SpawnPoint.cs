using UnityEngine;

namespace World
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private SpawnType spawnType = SpawnType.Empty;
        [SerializeField] private ObstacleSize size;
        [SerializeField] private float probability;
        
        public SpawnType SpawnType => spawnType;
        public ObstacleSize Size => size;
        public float Probability => probability;
    }
}
