using System.Collections.Generic;
using Core;
using Database;
using Gameplay.Effects;
using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Letters
{
    /// <summary>
    /// Manage the letters collection, filling words, replace words, apply effects
    /// </summary>
    public class LettersSystem : GameBehavior
    {
        [Header("Database")]
        [SerializeField] private WordDatabase wordsDatabase;
        [SerializeField] private LetterCell letterCellPrefab;
        
        [Header("References")]
        [SerializeField] private WordEffectRunner wordEffectRunner;
        
        [Header("Bonus")] 
        [SerializeField] private LettersDisplay[] bonusDisplays = new LettersDisplay[3];
        [SerializeField] private Color bonusHighlightColor = Colors.HighlightBonus;

        [Header("Malus")] 
        [SerializeField] private LettersDisplay[] malusDisplays = new LettersDisplay[3];
        [SerializeField] private Color malusHighlightColor = Colors.HighlightMalus;

        private readonly List<WordData> _currentBonus = new();
        private readonly List<WordData> _currentMalus = new();
        
        // Completed Words + Active Effect
        private readonly Queue<WordData> _completedWordsQueue = new();
        private WordEffect _activeEffect;

        private void OnEnable() 
        {
            GameEvents.OnLetterCollected += OnLetterCollected;
            GameEvents.OnVirusAttached += StopEffect;
        }

        private void OnDisable()
        {
            GameEvents.OnLetterCollected -= OnLetterCollected;
            GameEvents.OnVirusAttached -= StopEffect;
        }

        /// <summary>
        /// Initialize words panels (bonus / malus) with new words
        /// </summary>
        private void Start()
        {
            FillDisplays(bonusDisplays, _currentBonus, true);
            FillDisplays(malusDisplays, _currentMalus, false);
        }
        
        /// <summary>
        /// Manage when new letter collected
        /// Highlight letters on words
        /// Check completed words => add new word effect to the queue
        /// </summary>
        /// <param name="letter"></param>
        private void OnLetterCollected(string letter)
        {
            HighlightLetters(bonusDisplays, letter, bonusHighlightColor);
            HighlightLetters(malusDisplays, letter, malusHighlightColor);
            CheckCompletion(bonusDisplays, true);
            CheckCompletion(malusDisplays, false);
        }

        /// <summary>
        /// Highlight the same words letter as the letter collected
        /// </summary>
        /// <param name="displays"></param>
        /// <param name="letter"></param>
        /// <param name="color"></param>
        private void HighlightLetters(LettersDisplay[] displays, string letter, Color color)
        {
            foreach (LettersDisplay display in displays)
                display.HighlightLetters(letter, color);
        }

        /// <summary>
        /// Check if words completed => Add to the queue to effect be applied
        /// </summary>
        /// <param name="displays"></param>
        /// <param name="isBonus"></param>
        private void CheckCompletion(LettersDisplay[] displays, bool isBonus)
        {
            foreach (LettersDisplay display in displays)
            {
                if (!display.IsComplete()) 
                    continue;

                WordData completedWord = display.CurrentWordData;
                
                if (_completedWordsQueue.Contains(completedWord)) // Prevent the same completed word to be added multiple times
                    continue;

                if (isBonus)
                    GameEvents.OnAddScorePoints?.Invoke(completedWord.Word.Length * 100); // If completed word is a bonus word => Notify to add points to the current score

                _completedWordsQueue.Enqueue(completedWord);
            }
        }

        /// <summary>
        /// Add words in the bonus/malus panels
        /// </summary>
        /// <param name="displays"></param>
        /// <param name="currentWords"></param>
        /// <param name="isBonus"></param>
        private void FillDisplays(LettersDisplay[] displays, List<WordData> currentWords, bool isBonus)
        {
            foreach (LettersDisplay display in displays)
            {
                if (display.IsEmpty())
                    AssignWord(display, currentWords, isBonus);
            }
        }
        
        private void AssignWord(LettersDisplay display, List<WordData> currentWords, bool isBonus)
        {
            WordData word = wordsDatabase.GetRandomWordExcept(currentWords, isBonus);
            display.SetWord(word, letterCellPrefab);
            currentWords.Add(word);
        }
        
        private LettersDisplay FindDisplay(WordData word, LettersDisplay[] displays)
        {
            foreach (LettersDisplay display in displays)
                if (display.CurrentWordData == word) return display;
            return null;
        }

        /// <summary>
        /// Apply the word effect
        /// </summary>
        /// <param name="newEffect"></param>
        private void ApplyEffect(WordEffect newEffect)
        {
            if (!newEffect) return;
            _activeEffect = newEffect;
            _activeEffect.ApplyEffect(wordEffectRunner);
        }
        
        /// <summary>
        /// Stop the current word effect
        /// </summary>
        private void StopEffect()
        {
            wordEffectRunner.Stop();
            _activeEffect = null;
        }
        
        /// <summary>
        /// Manage the effects queue
        /// Check if current effect complete => run the next effect
        /// </summary>
        protected override void GameplayUpdate()
        {
            if (_activeEffect && _activeEffect.IsComplete) 
                _activeEffect = null;

            if (_activeEffect || _completedWordsQueue.Count == 0 || PlayerController.Instance.IsPlayerInfected()) 
                return;
            
            WordData nextWord = _completedWordsQueue.Dequeue();

            List<WordData> currentWords = nextWord.Effect.IsBonus ? _currentBonus : _currentMalus;
            LettersDisplay[] displays = nextWord.Effect.IsBonus ? bonusDisplays : malusDisplays;
            LettersDisplay display = FindDisplay(nextWord, displays);
            currentWords.Remove(nextWord);
            
            if (display)
                AssignWord(display, currentWords, nextWord.Effect.IsBonus);
            
            ApplyEffect(nextWord.Effect);
        }
    }
}