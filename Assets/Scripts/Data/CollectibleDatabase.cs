using System.Collections.Generic;
using Gameplay.Elements.Collectibles;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CollectibleDatabase", menuName = "SyntaxError/CollectibleDatabase")]
    public class CollectibleDatabase : ScriptableObject
    {
        [SerializeField] private LetterLoot letterLootPrefab;
        [SerializeField] private List<Collectible> groundedBonusLoots;
        [SerializeField] private List<Collectible> elevatedBonusLoots;
        
        public LetterLoot GetLetterLoot()
        {
            return letterLootPrefab;
        }
        
        public Collectible GetBonusLoot(bool grounded)
        {
            List<Collectible> bonusLoots = grounded ?  groundedBonusLoots : elevatedBonusLoots;
            return bonusLoots[Random.Range(0, bonusLoots.Count)];
        }
    }
}