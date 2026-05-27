using System.Collections.Generic;
using System.Linq;
using Gameplay.Elements.Collectibles;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CollectibleDatabase", menuName = "SyntaxError/CollectibleDatabase")]
    public class CollectibleDatabase : ScriptableObject
    {
        [SerializeField] private List<Collectible> collectibles;
        
        public Collectible GetPrefab()
        {
            return collectibles.First();
        }
    }
}