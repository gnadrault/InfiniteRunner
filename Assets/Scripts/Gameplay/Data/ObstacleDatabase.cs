using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(menuName = "SyntaxError/ObstacleDatabase")]
    public class ObstacleDatabase : ScriptableObject
    {
        [SerializeField] private List<ObstacleData> obstacles;

        public GameObject GetPrefab(ObstacleSize size, ObstacleType type)
        {
            ObstacleData obstacle = obstacles.FirstOrDefault(o => 
                o.size == size && o.type == type);
        
            return obstacle?.prefab;
        }
    }

    [System.Serializable]
    public class ObstacleData
    {
        public GameObject prefab;
        public ObstacleSize size;
        public ObstacleType type;
    }

    public enum ObstacleSize { One, Two, Three }
    public enum ObstacleType { Low, High, Full }
}