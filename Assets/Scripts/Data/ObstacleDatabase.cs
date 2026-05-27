using System.Collections.Generic;
using System.Linq;
using Gameplay.Elements.Obstacles;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ObstacleDatabase", menuName = "SyntaxError/ObstacleDatabase")]
    public class ObstacleDatabase : ScriptableObject
    {
        [SerializeField] private List<Obstacle> obstacles;

        public Obstacle GetPrefab(ObstacleType type, ObstacleSize size, bool isMobile)
        {
            Obstacle obstacle = obstacles.FirstOrDefault(o => 
                o.Size == size && o.Type == type && o.IsMobile == isMobile);
        
            return obstacle;
        }
    }
    
}