using System;
using System.Collections.Generic;
using Data;
using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Letters
{
    public class LettersSystem : MonoBehaviour
    {
        public static event Action<WordData[]> OnActiveWordsChanged;

        [SerializeField] private WordDatabase wordsDatabase;
        [SerializeField] private LetterCell letterCellPrefab;
        [SerializeField] private PlayerController player;

        [Header("Bonus")] [SerializeField] private LettersDisplay[] bonusDisplays = new LettersDisplay[3];
        [SerializeField] private Color bonusHighlight = Colors.HighlightBonus;

        [Header("Malus")] [SerializeField] private LettersDisplay[] malusDisplays = new LettersDisplay[3];
        [SerializeField] private Color malusHighlight = Colors.HighlightMalus;

        private readonly List<WordData> _currentBonus = new();
        private readonly List<WordData> _currentMalus = new();
        private readonly Queue<WordData> _completedWordsQueue = new();
        private World.GameElement.WordEffect.WordEffect _activeEffect;

        private void OnEnable() => GameEvents.OnLetterCollected += OnLetterCollected;
        private void OnDisable() => GameEvents.OnLetterCollected -= OnLetterCollected;

        private void Start()
        {
            FillDisplays(bonusDisplays, _currentBonus, true);
            FillDisplays(malusDisplays, _currentMalus, false);
            FireActiveWordsChanged();
        }

        private void Update()
        {
            ProcessEffectQueue();
        }
        
        private void ProcessEffectQueue()
        {
            if (_activeEffect && !_activeEffect.isComplete) return;
            if (_completedWordsQueue.Count == 0) return;

            WordData next = _completedWordsQueue.Dequeue();

            List<WordData> currentWords = next.isBonus ? _currentBonus : _currentMalus;
            LettersDisplay[] displays = next.isBonus ? bonusDisplays : malusDisplays;

            LettersDisplay display = FindDisplay(next, displays);
            currentWords.Remove(next);
            if (display)
                AssignWord(display, currentWords, next.isBonus);

            FireActiveWordsChanged();

            if (!next.effect) return;

            _activeEffect = next.effect;
            _activeEffect.ApplyEffect(player, this);
        }

        private LettersDisplay FindDisplay(WordData word, LettersDisplay[] displays)
        {
            foreach (LettersDisplay display in displays)
                if (display.CurrentWordData == word) return display;
            return null;
        }

        private void OnLetterCollected(string letter)
        {
            GameEvents.OnAddScorePoints?.Invoke(30);

            HighlightLetters(bonusDisplays, letter, bonusHighlight);
            HighlightLetters(malusDisplays, letter, malusHighlight);

            CheckCompletion(bonusDisplays, true);
            CheckCompletion(malusDisplays, false);

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
                if (!display.IsComplete()) continue;

                WordData completedWord = display.CurrentWordData;

                if (isBonus)
                    GameEvents.OnAddScorePoints?.Invoke(completedWord.word.Length * 100);
                
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

        private void FireActiveWordsChanged()
        {
            List<WordData> all = new List<WordData>(_currentBonus);
            all.AddRange(_currentMalus);
            OnActiveWordsChanged?.Invoke(all.ToArray());
        }
    }
}