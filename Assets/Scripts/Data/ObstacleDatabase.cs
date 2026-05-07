using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using World.GameElement.Obstacle;

namespace Data
{
    [CreateAssetMenu(fileName = "ObstacleDatabase", menuName = "SyntaxError/ObstacleDatabase")]
    public class ObstacleDatabase : ScriptableObject
    {
        [SerializeField] private List<ObstacleElement> obstacles;

        public ObstacleElement GetPrefab(ObstacleType type, ObstacleSize size, bool isMobile)
        {
            ObstacleElement obstacle = obstacles.FirstOrDefault(o => 
                o.Size == size && o.Type == type && o.IsMobile == isMobile);
        
            return obstacle;
        }
    }
    
}