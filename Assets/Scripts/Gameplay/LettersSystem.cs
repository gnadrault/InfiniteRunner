using System.Collections.Generic;
using Core;
using Data;
using Effects;
using Gameplay.Letters;
using Player;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class LettersSystem : GameBehavior
    {
        [SerializeField] private WordDatabase wordsDatabase;
        [SerializeField] private LetterCell letterCellPrefab;
        [SerializeField] private WordEffectRunner wordEffectRunner;

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
        
        // DEBUG
        public WordEffect ActiveEffect => _activeEffect;
        public Queue<WordData> CompletedQueue => _completedWordsQueue;

        private void Start()
        {
            FillDisplays(bonusDisplays, _currentBonus, true);
            FillDisplays(malusDisplays, _currentMalus, false);
        }
        
        private void OnLetterCollected(string letter)
        {
            HighlightLetters(bonusDisplays, letter, bonusHighlightColor);
            HighlightLetters(malusDisplays, letter, malusHighlightColor);
            CheckCompletion(bonusDisplays, true);
            CheckCompletion(malusDisplays, false);
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
                
                if (_completedWordsQueue.Contains(completedWord))
                    continue;

                if (isBonus)
                    GameEvents.OnAddScorePoints?.Invoke(completedWord.Word.Length * 100);

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
            _activeEffect.ApplyEffect(wordEffectRunner);
        }
        
        private void StopEffect()
        {
            if (!_activeEffect) return;
            _activeEffect.RemoveEffect();
            _activeEffect = null;
        }
        
        protected override void GameplayUpdate()
        {
            if (_activeEffect && _activeEffect.IsComplete) 
                _activeEffect = null;

            if (_activeEffect || _completedWordsQueue.Count == 0 || PlayerController.Instance.IsPlayerInfected()) 
                return;
            
            WordData nextWord = _completedWordsQueue.Dequeue();

            List<WordData> currentWords = nextWord.IsBonus ? _currentBonus : _currentMalus;
            LettersDisplay[] displays = nextWord.IsBonus ? bonusDisplays : malusDisplays;
            LettersDisplay display = FindDisplay(nextWord, displays);
            currentWords.Remove(nextWord);
            
            if (display)
                AssignWord(display, currentWords, nextWord.IsBonus);
            
            ApplyEffect(nextWord.Effect);
        }
    }
}