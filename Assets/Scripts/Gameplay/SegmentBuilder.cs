using Core;
using Data;
using Gameplay.Elements.Collectibles;
using Gameplay.Elements.Enemies;
using Gameplay.Segments;
using Gameplay.Segments.Spawn;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class SegmentBuilder : GameBehavior
    {
        [Header("Databases")] 
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
                    case LetterSpawnPoint letterSpawnPoint:
                        GenerateLetterObject(letterSpawnPoint, currentPhase);
                        break;
                    case BonusSpawnPoint bonusSpawnPoint:
                        GenerateBonusObject(bonusSpawnPoint, currentPhase);
                        break;
                    case VirusSpawnPoint virusSpawnPoint:
                        GenerateVirusObject(virusSpawnPoint, currentPhase);
                        break;
                }
            }
        }
        
        private void GenerateLetterObject(LetterSpawnPoint spawnPoint, PhaseData currentPhase)
        {
            Collectible element = collectibleDatabase.GetLetterLoot();
            Instantiate(element, spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
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