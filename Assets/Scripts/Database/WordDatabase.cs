using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Database
{
    /// <summary>
    /// Words effects Database
    /// </summary>
    [CreateAssetMenu(fileName = "WordEffectDatabase", menuName = "SyntaxError/WordEffectDatabase")]
    public class WordEffectDatabase : ScriptableObject
    {
        [Header("References")]
        [SerializeField] private List<WordEffect> wordsEffect;

        public WordEffect GetRandomWordExcept(List<WordEffect> exceptWords, bool bonus)
        {
            List<WordEffect> availableWords =
                wordsEffect.Where(word => !exceptWords.Contains(word) && word.Effect.IsBonus == bonus).ToList();
            return availableWords[Random.Range(0, availableWords.Count)];
        }

        public WordEffect GetRandomWord(bool bonus)
        {
            List<WordEffect> availableWords = wordsEffect.Where(word => word.Effect.IsBonus == bonus).ToList();
            return availableWords[Random.Range(0, availableWords.Count)];
        }
    }
}