using System;
using System.Collections.Generic;
using Data;
using Gameplay.Letters;
using UnityEngine;
using World.GameElement.Collectible;
using World.GameElement.Obstacle;
using World.GameElement.Virus;
using World.Spawn;
using Random = UnityEngine.Random;

namespace World.Segment
{
    public class SegmentBuilder : MonoBehaviour
    {
        [Header("Databases")] 
        [SerializeField] private ObstacleDatabase obstacleDatabase;
        [SerializeField] private CollectibleDatabase collectibleDatabase;
        [SerializeField] private VirusDatabase virusDatabase;

        [Header("Settings")] 
        [SerializeField] private int activeLettersSpawnRate = 3;

        private WordData[] _activeWords;

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

        public void GenerateSegmentObjects(Segment segment, PhaseState currentPhase)
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

        private void GenerateObstacleObject(ObstacleSpawnPoint spawnPoint, PhaseState currentPhase)
        {
            ObstacleElement obstacleElement = obstacleDatabase.GetPrefab(spawnPoint.Type, spawnPoint.Size, spawnPoint.IsMobile);
            Instantiate(obstacleElement, spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
        }

        private void GenerateCollectibleObject(CollectibleSpawnPoint spawnPoint, PhaseState currentPhase)
        {
            CollectibleElement element = collectibleDatabase.GetPrefab();
            Letter letterSpawned = (Letter)Instantiate(element, spawnPoint.transform.position, Quaternion.identity,
                spawnPoint.transform);
            letterSpawned.SetLabelText(GetRandomLetter().ToString());
        }

        private void GenerateVirusObject(VirusSpawnPoint spawnPoint, PhaseState currentPhase)
        {
            VirusElement prefab = virusDatabase.GetPrefab();
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
                    foreach (char c in word.word)
                        pool.Add(c);
                }

            return pool[Random.Range(0, pool.Count)];
        }
    }
}