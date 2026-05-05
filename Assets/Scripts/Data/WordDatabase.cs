using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "WordDatabase", menuName = "SyntaxError/WordDatabase")]
    public class WordDatabase : ScriptableObject
    {
        [SerializeField] private List<WordData> words;
        
        public WordData GetRandomWordExcept(List<WordData> exceptWords)
        {
            List<WordData> availableWords = words.Where(w => !exceptWords.Contains(w)).ToList();
            return availableWords[Random.Range(0, availableWords.Count)];
        }
    }
    
    [System.Serializable]
    public struct WordData
    {
        public string word;
        public WordEffect effect;
    }
}