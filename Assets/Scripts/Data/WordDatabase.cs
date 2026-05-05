using System;
using System.Collections.Generic;
using System.Linq;
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
    
    [System.Serializable]
    public struct WordData : IEquatable<WordData>
    {
        public string word;
        public WordEffect effect;

        public bool Equals(WordData other)
        {
            return word == other.word && effect == other.effect;
        }

        public override bool Equals(object obj)
        {
            return obj is WordData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(word, (int)effect);
        }
    }
}