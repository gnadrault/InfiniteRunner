using System.Collections.Generic;
using UnityEngine;
using World.Data;
using World.GameElement;

namespace World
{
    public class SegmentGeneration : MonoBehaviour
    {
        [Header("Obstacles")]
        [SerializeField] private ObstacleList[] obstableList = new ObstacleList[3];
        [SerializeField] private List<Obstacle> obstaclesMobilePrefab;
        
        public void GenerateSegmentObjects(Segment segment)
        {
            print("Number of spawn " + segment.SpawnPoints.Count);
            foreach (SpawnPoint spawnPoint in segment.SpawnPoints)
            {
                switch (spawnPoint.SpawnType)
                {
                    case SpawnType.Obstacle:
                        print("Obstacle");
                        GenerateObstacleObject(spawnPoint);
                        break;
                    case SpawnType.Empty:
                        print("Empty");
                        break;
                    default:
                        print("Undefined");
                        break;
                }
            }
        }

        private void GenerateObstacleObject(SpawnPoint spawnPoint)
        {
            int listIndex = (int)spawnPoint.Size;
            print ("Size " + spawnPoint.Size);
            int randomObstacleIndex = Random.Range(0, obstableList[listIndex].obstacles.Count); // Get random obstacle from the obstacle list
            Obstacle obstacle = obstableList[listIndex].obstacles[randomObstacleIndex];
            Instantiate(obstacle, spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
        }

        private void GenerateCollectibleObject(SpawnPoint spawnPoint)
        {
            
        }

        private void GenerateVirusObject(SpawnPoint spawnPoint)
        {
            
        }
        
    }
}
