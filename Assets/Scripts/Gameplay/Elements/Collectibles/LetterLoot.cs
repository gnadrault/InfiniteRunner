using System;
using System.Collections.Generic;
using Data;
using Player;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Elements.Collectibles
{
    public class LetterLoot : Collectible
    {
        [SerializeField] private TextMeshPro label;

        public string Label => label.text;

        private void Awake()
        {
            label.text = GetRandomLetter().ToString();
        }

        public override void OnPlayerCollision(Transform position)
        {
            PlayerController.Instance.CollectLetter(this);
            Destroy(gameObject);
        }
        
        private char GetRandomLetter()
        {
            List<char> pool = new List<char>();

            for (char c = 'A'; c <= 'Z'; c++)
                pool.Add(c);
            
            return pool[Random.Range(0, pool.Count)];
        }
        
        /*
        private WordData[] _activeWords = Array.Empty<WordData>();

        private void SetActiveWords(WordData[] words)
        {
            _activeWords = words;
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
        }*/
    }
}