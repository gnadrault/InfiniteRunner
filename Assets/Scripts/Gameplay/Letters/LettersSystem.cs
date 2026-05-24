using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Utils;
using WordEffect = World.GameElement.WordEffect.WordEffect;

namespace Gameplay.Letters
{
    public class LettersSystem : MonoBehaviour
    {
        public static event Action<WordData[]> OnActiveWordsChanged;

        [SerializeField] private WordDatabase wordsDatabase;
        [SerializeField] private LetterCell letterCellPrefab;

        [Header("Bonus")]
        [SerializeField] private LettersDisplay[] bonusDisplays = new LettersDisplay[3];
        [SerializeField] private Color bonusHighlight = Colors.HighlightBonus;

        [Header("Malus")] 
        [SerializeField] private LettersDisplay[] malusDisplays = new LettersDisplay[3];
        [SerializeField] private Color malusHighlight = Colors.HighlightMalus;

        private readonly List<WordData> _currentBonus = new();
        private readonly List<WordData> _currentMalus = new();
        
        private readonly List<WordData> _currentWordsQueue = new();
        private WordData _currentWord;

        private void OnEnable()
        {
            GameEvents.OnLetterCollected += OnLetterCollected;
        }

        private void OnDisable()
        {
            GameEvents.OnLetterCollected -= OnLetterCollected;
        }

        private void Start()
        {
            FillDisplays(bonusDisplays, _currentBonus, true);
            FillDisplays(malusDisplays, _currentMalus, false);
            FireActiveWordsChanged();
        }

        private void OnLetterCollected(string letter)
        {
            GameEvents.OnAddScorePoints?.Invoke(30);
            
            HighlightLetters(bonusDisplays, letter, bonusHighlight);
            HighlightLetters(malusDisplays, letter, malusHighlight);

            CheckCompletion(bonusDisplays, _currentBonus, true);
            CheckCompletion(malusDisplays, _currentMalus, false);

            FireActiveWordsChanged();
        }

        private void HighlightLetters(LettersDisplay[] displays, string letter, Color color)
        {
            foreach (LettersDisplay display in displays)
                display.HighlightLetters(letter, color);
        }

        private void CheckCompletion(LettersDisplay[] displays, List<WordData> currentWords, bool isBonus)
        {
            foreach (LettersDisplay display in displays)
            {
                if (!display.IsComplete()) continue;

                if (isBonus)
                    GameEvents.OnAddScorePoints?.Invoke(display.CurrentWordData.word.Length * 100);

                // TODO apply effect completed word => Then remove the word from current
                _currentWordsQueue.Add(display.CurrentWordData);

                currentWords.Remove(display.CurrentWordData);
                AssignWord(display, currentWords, isBonus);
            }
        }

        private void FillDisplays(LettersDisplay[] displays, List<WordData> currentWords, bool isBonus)
        {
            foreach (var display in displays)
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

        private void ApplyEffect()
        {
            if (_currentWord != null && _currentWord.effect.isDone)
            {
                if (_currentWord.isBonus)
                {
                    _currentBonus.Remove(_currentWord);
                    //AssignWord(bonusDisplays[], _currentBonus, _currentWord.isBonus);
                }
                else
                {
                    _currentMalus.Remove(_currentWord);
                    //AssignWord(malusDisplays[], _currentMalus, _currentWord.isBonus);
                }
            }
        }

        private void Update()
        {
            ApplyEffect();
        }
    }
}