using System;
using System.Collections.Generic;
using System.Linq;
using Effects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data
{
    /// <summary>
    /// Words effects Database
    /// </summary>
    [CreateAssetMenu(fileName = "WordDatabase", menuName = "SyntaxError/WordDatabase")]
    public class WordDatabase : ScriptableObject
    {
        [Header("References")]
        [SerializeField] private List<WordData> bonusWords;
        [SerializeField] private List<WordData> malusWords;

        public WordData GetRandomWordExcept(List<WordData> exceptWords, bool isBonus)
        {
            List<WordData> words = isBonus ? bonusWords : malusWords;
            List<WordData> availableWords = words.Where(w => !exceptWords.Contains(w)).ToList();
            return availableWords[Random.Range(0, availableWords.Count)];
        }

        public WordData GetRandomWord(bool isBonus)
        {
            List<WordData> words = isBonus ? bonusWords : malusWords;
            return words[Random.Range(0, words.Count)];
        }
    }
    
    [Serializable]
    public class WordData
    {
        [SerializeField] private string word;
        [SerializeField] private WordEffect effect;

        public string Word => word;
        public WordEffect Effect => effect;
    }
}