using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Elements.Collectibles
{
    /// <summary>
    /// Letter collectible game objects
    /// </summary>
    public class LetterLoot : Collectible
    {
        [Header("References")]
        [SerializeField] private TextMeshPro label;

        public string Label => label.text;

        private void Awake()
        {
            label.text = GetRandomLetter().ToString(); // Set the random letter to the display text
        }

        public override void OnPlayerCollision(Transform position)
        {
            PlayerController.Instance.CollectLetter(this);
            Destroy(gameObject);
        }
        
        /// <summary>
        /// Get a random letter between A-Z
        /// </summary>
        /// <returns></returns>
        private char GetRandomLetter()
        {
            List<char> pool = new List<char>();

            for (char c = 'A'; c <= 'Z'; c++)
                pool.Add(c);
            
            return pool[Random.Range(0, pool.Count)];
        }
    }
}