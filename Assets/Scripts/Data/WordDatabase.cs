using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.GameElement.WordEffect;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data
{
    [CreateAssetMenu(fileName = "WordDatabase", menuName = "SyntaxError/WordDatabase")]
    public class WordDatabase : ScriptableObject
    {
        [SerializeField] private List<WordData> bonusWords;
        [SerializeField] private List<WordData> malusWords;

        public WordData GetRandomWordExcept(List<WordData> exceptWords, bool isBonus)
        {
            List<WordData> words = isBonus ? bonusWords : malusWords;
            List<WordData> availableWords = words.Where(w => !exceptWords.Contains(w)).ToList();
            return availableWords[Random.Range(0, availableWords.Count)];
        }
    }
    
    [Serializable]
    public class WordData
    {
        public string word;
        public bool isBonus;
        public WordEffect effect;
    }
}