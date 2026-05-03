using System.Collections.Generic;
using UnityEngine;
using World.GameElement;
using World.Spawn;

namespace World.Segment
{
    public class SegmentGeneration : MonoBehaviour
    {
        [Header("Obstacles")] 
        [SerializeField] private List<Obstacle> obstaclesFixedPrefab;
        [SerializeField] private List<Obstacle> obstaclesMobilePrefab;

        [Header("Collectibles")]
        [SerializeField] private Letter letterPrefab;
        
        public void GenerateSegmentObjects(Segment segment, int phaseIndex)
        {
            foreach (SpawnPoint spawnPoint in segment.SpawnPoints)
            {
                if (!spawnPoint.Element) continue; // No element => Go to next spawn point
                switch (spawnPoint.Element.SpawnType)
                {
                    case SpawnType.Obstacle:
                        GenerateObstacleObject((Obstacle)spawnPoint.Element, phaseIndex);
                        break;
                    case SpawnType.Collectible:
                        GenerateCollectibleObject((Collectible)spawnPoint.Element, phaseIndex);
                        break;
                    case SpawnType.Virus:
                        GenerateVirusObject((Virus)spawnPoint.Element, phaseIndex);
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
            Letter letterSpawned = Instantiate(letterPrefab, element.transform.position, Quaternion.identity, element.transform);
            letterSpawned.SetLabelText("E"); // TODO Set letters values depending on words
        }

        private void GenerateVirusObject(Virus element, int phaseIndex)
        {
            
        }
    }
}