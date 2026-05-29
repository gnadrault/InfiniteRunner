using System;
using System.Collections.Generic;
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
    public class SegmentBuilder : MonoBehaviour
    {
        [Header("Databases")] 
        [SerializeField] private ObstacleDatabase obstacleDatabase;
        [SerializeField] private CollectibleDatabase collectibleDatabase;

        [Header("Settings")] 
        [SerializeField] private int activeLettersSpawnRate = 3;

        private WordData[] _activeWords = Array.Empty<WordData>();

        private void OnEnable()
        {
            LettersSystem.OnActiveWordsChanged += SetActiveWords;
        }

        private void OnDisable()
        {
            LettersSystem.OnActiveWordsChanged -= SetActiveWords;
        }

        private void SetActiveWords(WordData[] words)
        {
            _activeWords = words;
        }

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
                    case CollectibleSpawnPoint collectibleSpawnPoint:
                        GenerateCollectibleObject(collectibleSpawnPoint, currentPhase);
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

        private void GenerateCollectibleObject(CollectibleSpawnPoint spawnPoint, PhaseData currentPhase)
        {
            Collectible element = collectibleDatabase.GetPrefab();
            Letter letterSpawned = (Letter)Instantiate(element, spawnPoint.transform.position, Quaternion.identity,
                spawnPoint.transform);
            letterSpawned.SetLabelText(GetRandomLetter().ToString());
        }

        private void GenerateVirusObject(VirusSpawnPoint spawnPoint, PhaseData currentPhase)
        {
            Virus prefab = currentPhase.Virus[Random.Range(0, currentPhase.Virus.Count)];
            Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
        }

        private char GetRandomLetter()
        {
            List<char> pool = new List<char>();

            for (char c = 'A'; c <= 'Z'; c++)
                pool.Add(c);
            
            foreach (WordData word in _activeWords)
                for (int i = 0; i < activeLettersSpawnRate; i++)
                {
                    foreach (char c in word.Word)
                        pool.Add(c);
                }
            return pool[Random.Range(0, pool.Count)];
        }
    }
}