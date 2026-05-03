using System.Collections.Generic;
using UnityEngine;
using World.Data;
using World.GameElement;

namespace World.Segment
{
    public class SegmentGeneration : MonoBehaviour
    {
        [Header("Obstacles")] 
        [SerializeField] private List<Obstacle> obstaclesFixedPrefab;
        [SerializeField] private List<Obstacle> obstaclesMobilePrefab;

        public void GenerateSegmentObjects(Data.Segment segment, int phaseIndex)
        {
            foreach (SpawnPoint spawnPoint in segment.SpawnPoints)
            {
                switch (spawnPoint.Element.SpawnType)
                {
                    case SpawnType.Obstacle:
                        print("Obstacle");
                        GenerateObstacleObject((Obstacle)spawnPoint.Element, phaseIndex);
                        break;
                    case SpawnType.Collectible:
                        print("Collectible");
                        GenerateCollectibleObject((Collectible)spawnPoint.Element, phaseIndex);
                        break;
                    case SpawnType.Virus:
                        print("Virus");
                        GenerateVirusObject((Virus)spawnPoint.Element, phaseIndex);
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

        private void GenerateObstacleObject(Obstacle element, int phaseIndex)
        {
            Obstacle obstacle =
                obstaclesFixedPrefab.Find(o =>
                    o.Type == element.Type && o.Size == element.Size);
            Instantiate(obstacle, element.transform.position, Quaternion.identity, element.transform);
        }

        private void GenerateCollectibleObject(Collectible element, int phaseIndex)
        {
        }

        private void GenerateVirusObject(Virus element, int phaseIndex)
        {
            
        }
    }
}