using System.Collections.Generic;
using Gameplay;
using Gameplay.Data;
using UnityEngine;
using World.GameElement.Collectible;
using World.GameElement.Obstacle;
using World.GameElement.Virus;
using World.Spawn;

namespace World.Segment
{
    public class SegmentGeneration : MonoBehaviour
    {
        [Header("Obstacles")] 
        [SerializeField] private List<ObstacleElement> obstaclesFixedPrefab;
        [SerializeField] private List<ObstacleElement> obstaclesMobilePrefab;

        [Header("Collectibles")]
        [SerializeField] private Letter letterPrefab;
        [SerializeField] private int activeLettersSpawnRate = 3;
        
        [Header("Virus")]
        [SerializeField] private VirusDatabase virusDatabase;
        
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
                        GenerateObstacleObject((ObstacleElement)spawnPoint.Element, phaseIndex);
                        break;
                    case SpawnType.Collectible:
                        GenerateCollectibleObject((CollectibleElement)spawnPoint.Element, phaseIndex);
                        break;
                    case SpawnType.Virus:
                        GenerateVirusObject((VirusElement)spawnPoint.Element, phaseIndex);
                        break;
                    default:
                        print("Undefined");
                        break;
                }
            }
        }

        private void GenerateObstacleObject(ObstacleElement element, int phaseIndex)
        {
            ObstacleElement obstacleElement =
                obstaclesFixedPrefab.Find(o =>
                    o.Type == element.Type && o.Size == element.Size);
            Instantiate(obstacleElement, element.transform.position, Quaternion.identity, element.transform);
        }

        private void GenerateCollectibleObject(CollectibleElement element, int phaseIndex)
        {
            Letter letterSpawned = Instantiate(letterPrefab, element.transform.position, Quaternion.identity, element.transform);
            letterSpawned.SetLabelText(GetRandomLetter().ToString());
        }

        private void GenerateVirusObject(VirusElement element, int phaseIndex)
        {
            VirusElement prefab = virusDatabase.GetPrefab(element);
            Instantiate(prefab, element.transform.position, Quaternion.identity, element.transform);
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