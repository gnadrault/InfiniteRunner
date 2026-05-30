using System;
using System.Collections.Generic;
using Data;
using Gameplay.Elements.Effects;
using Gameplay.Letters;
using Player;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class LettersSystem : MonoBehaviour
    {
        [SerializeField] private WordDatabase wordsDatabase;
        [SerializeField] private LetterCell letterCellPrefab;
        [SerializeField] private PlayerController player;

        // Bonus / Malus
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
        private readonly HashSet<WordData> _enqueuedWords = new();
        private WordEffect _activeEffect;
        
        public static event Action<WordData[]> OnActiveWordsChanged;

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
        
        // DEBUG
        public WordEffect ActiveEffect => _activeEffect;
        public Queue<WordData> CompletedQueue => _completedWordsQueue;

        private void Start()
        {
            FillDisplays(bonusDisplays, _currentBonus, true);
            FillDisplays(malusDisplays, _currentMalus, false);
            FireActiveWordsChanged();
        }
        
        private void OnLetterCollected(string letter, bool multiplier)
        {
            HighlightLetters(bonusDisplays, letter, bonusHighlightColor);
            HighlightLetters(malusDisplays, letter, malusHighlightColor);
            CheckCompletion(bonusDisplays, true);
            CheckCompletion(malusDisplays, false);
            ProcessEffectQueue();
            FireActiveWordsChanged();
        }

        private void HighlightLetters(LettersDisplay[] displays, string letter, Color color)
        {
            foreach (LettersDisplay display in displays)
                display.HighlightLetters(letter, color);
        }

        private void CheckCompletion(LettersDisplay[] displays, bool isBonus)
        {
            foreach (LettersDisplay display in displays)
            {
                if (!display.IsComplete()) 
                    continue;

                WordData completedWord = display.CurrentWordData;

                if (!_enqueuedWords.Add(completedWord))
                    continue;

                if (isBonus)
                    GameEvents.OnWordCompleted?.Invoke(completedWord.Word.Length);

                _completedWordsQueue.Enqueue(completedWord);
            }
        }

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

        private void Update()
        {
            ProcessEffectQueue();
        }
        
        private void ProcessEffectQueue()
        {
            if (_activeEffect && _activeEffect.isComplete) 
                _activeEffect = null;

            if (_activeEffect || _completedWordsQueue.Count == 0 || player.IsPlayerInfected()) 
                return;
            
            // New effect from completed word (Queue)
            WordData nextWord = _completedWordsQueue.Dequeue();
            _enqueuedWords.Remove(nextWord);

            List<WordData> currentWords = nextWord.IsBonus ? _currentBonus : _currentMalus;
            LettersDisplay[] displays = nextWord.IsBonus ? bonusDisplays : malusDisplays;

            LettersDisplay display = FindDisplay(nextWord, displays);
            currentWords.Remove(nextWord);
            
            if (display)
                AssignWord(display, currentWords, nextWord.IsBonus);
            
            ApplyEffect(nextWord.Effect);
        }
        
        private LettersDisplay FindDisplay(WordData word, LettersDisplay[] displays)
        {
            foreach (LettersDisplay display in displays)
                if (display.CurrentWordData == word) return display;
            return null;
        }

        private void ApplyEffect(WordEffect newEffect)
        {
            if (!newEffect) return;
            _activeEffect = newEffect;
            _activeEffect.ApplyEffect(player, this);
        }
        
        private void StopEffect()
        {
            if (!_activeEffect) return;
            _activeEffect.RemoveEffect();
            _activeEffect = null;
        }
        
        /**
         * Event with all current words (used to spawn specific letters)
         */
        private void FireActiveWordsChanged()
        {
            List<WordData> all = new List<WordData>(_currentBonus);
            all.AddRange(_currentMalus);
            OnActiveWordsChanged?.Invoke(all.ToArray());
        }
    }
}