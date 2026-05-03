using System.Collections.Generic;
using Gameplay;
using Gameplay.Data;
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
            letterSpawned.SetLabelText(GetRandomLetter().ToString());
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

        private void GenerateVirusObject(Virus element, int phaseIndex)
        {
            
        }
    }
}