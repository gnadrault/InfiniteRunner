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

        [Header("Bonus")]
        [SerializeField] private LettersDisplay[] bonusDisplays = new LettersDisplay[3];
        [SerializeField] private Color bonusHighlight = Colors.HighlightBonus;

        [Header("Malus")] 
        [SerializeField] private LettersDisplay[] malusDisplays = new LettersDisplay[3];
        [SerializeField] private Color malusHighlight = Colors.HighlightMalus;

        private readonly List<WordData> _currentBonus = new();
        private readonly List<WordData> _currentMalus = new();

        private void OnEnable()
        {
            PlayerController.OnLetterCollected += OnLetterCollected;
        }

        private void OnDisable()
        {
            PlayerController.OnLetterCollected -= OnLetterCollected;
        }

        private void Start()
        {
            FillDisplays(bonusDisplays, _currentBonus, true);
            FillDisplays(malusDisplays, _currentMalus, false);
            FireActiveWordsChanged();
        }

        private void OnLetterCollected(string letter)
        {
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

                // TODO apply effect completed word => Then remove the word from current
                // ApplyEffect(display.CurrentWord.effect); 

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
    }
}