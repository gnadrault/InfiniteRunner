using System.Collections.Generic;
using System.Linq;
using Gameplay.GameElement.Collectible;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CollectibleDatabase", menuName = "SyntaxError/CollectibleDatabase")]
    public class CollectibleDatabase : ScriptableObject
    {
        [SerializeField] private List<CollectibleElement> collectibles;
        
        public CollectibleElement GetPrefab()
        {
            return collectibles.First();
        }
    }
}