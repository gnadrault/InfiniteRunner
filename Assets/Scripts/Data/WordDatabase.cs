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
    public class WordData : IEquatable<WordData>
    {
        public readonly string word;
        public readonly bool isBonus;
        public readonly World.GameElement.WordEffect.WordEffect effect;

        public WordData(bool isBonus, string word, World.GameElement.WordEffect.WordEffect effect)
        {
            this.isBonus = isBonus;
            this.word = word;
            this.effect = effect;
        }

        public bool Equals(WordData other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return word == other.word && isBonus == other.isBonus && Equals(effect, other.effect);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((WordData)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(word, isBonus, effect);
        }
    }
}