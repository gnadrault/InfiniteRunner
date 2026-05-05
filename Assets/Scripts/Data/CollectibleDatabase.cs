using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using World.GameElement.Collectible;

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