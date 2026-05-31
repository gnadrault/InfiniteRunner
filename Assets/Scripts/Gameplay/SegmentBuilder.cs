using System;
using Core;
using Data;
using Gameplay.Elements.Collectibles;
using Gameplay.Elements.Enemies;
using Gameplay.Elements.Obstacles;
using Gameplay.Segments;
using Gameplay.Segments.Spawn;
using Movement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class SegmentBuilder : GameBehavior
    {
        [Header("Databases")] 
        [SerializeField] private ObstacleDatabase obstacleDatabase;
        [SerializeField] private CollectibleDatabase collectibleDatabase;

        [Header("Settings")]
        [SerializeField] private int activeLettersSpawnRate = 3;

        
        public void GenerateSegmentObjects(Segment segment, PhaseData currentPhase)
        {
            foreach (SpawnPoint spawnPoint in segment.SpawnPoints)
            {
                if (spawnPoint is EmptySpawnPoint) continue; // No element => Go to next spawn point
                switch (spawnPoint)
                {
                    case ObstacleSpawnPoint obstacleSpawnPoint:
                        GenerateObstacleObject(obstacleSpawnPoint, currentPhase);
                        break;
                    case LetterSpawnPoint letterSpawnPoint:
                        GenerateLetterObject(letterSpawnPoint, currentPhase);
                        break;
                    case BonusSpawnPoint bonusSpawnPoint:
                        GenerateBonusObject(bonusSpawnPoint, currentPhase);
                        break;
                    case VirusSpawnPoint virusSpawnPoint:
                        GenerateVirusObject(virusSpawnPoint, currentPhase);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        private void GenerateObstacleObject(ObstacleSpawnPoint spawnPoint, PhaseData currentPhase)
        {
            Obstacle obstacle =
                obstacleDatabase.GetPrefab(spawnPoint.Type, spawnPoint.Size, spawnPoint.IsMobile);
            obstacle = Instantiate(obstacle, spawnPoint.transform.position, Quaternion.identity,
                spawnPoint.transform);
            if (obstacle.TryGetComponent(out FallingObject falling))
            {
                falling.Initialize(currentPhase.Speed);
            }
        }
        
        private void GenerateLetterObject(LetterSpawnPoint spawnPoint, PhaseData currentPhase)
        {
            Collectible element = collectibleDatabase.GetLetterLoot();
            Instantiate(element, spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
            /*Letter letterSpawned = (Letter)Instantiate(element, spawnPoint.transform.position, Quaternion.identity,
                spawnPoint.transform);
            letterSpawned.SetLabelText(GetRandomLetter().ToString());*/
        }

        private void GenerateBonusObject(BonusSpawnPoint spawnPoint, PhaseData currentPhase)
        {
            Collectible element = collectibleDatabase.GetBonusLoot(spawnPoint.IsElevated);
            Instantiate(element, spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
        }

        private void GenerateVirusObject(VirusSpawnPoint spawnPoint, PhaseData currentPhase)
        {
            Virus prefab = currentPhase.Virus[Random.Range(0, currentPhase.Virus.Count)];
            Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
        }
    }
}