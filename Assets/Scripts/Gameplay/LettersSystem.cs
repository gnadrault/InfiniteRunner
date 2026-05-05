using System;
using System.Collections.Generic;
using Data;
using Player;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class LettersSystem : MonoBehaviour
    {
        [SerializeField] private WordDatabase bonusWords;
        [SerializeField] private WordDatabase malusWords;
        [SerializeField] private LetterCell letterCellPrefab;
        [SerializeField] private LettersDisplay[] bonusWordsDisplays = new LettersDisplay[3];
        [SerializeField] private LettersDisplay[] malusWordsDisplays = new LettersDisplay[3];

        // TODO inheritance, bonus / malus

        private List<WordData> _currentWords = new();
        public static event Action<WordData[]> OnActiveWordsChanged;

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
            Init();
        }

        private void Init()
        {
            AddWords(bonusWordsDisplays, bonusWords);
            AddWords(malusWordsDisplays, malusWords);
        }

        private void OnLetterCollected(string letter)
        {
            ActivateLetters(bonusWordsDisplays, letter, Colors.HighlightBonus);
            ActivateLetters(malusWordsDisplays, letter, Colors.HighlightMalus);
            AddWords(bonusWordsDisplays, bonusWords);
            AddWords(malusWordsDisplays, malusWords);
        }

        private void ActivateLetters(LettersDisplay[] lettersDisplays, string letterCollected, Color color)
        {
            foreach (LettersDisplay lettersDisplay in lettersDisplays)
            {
                lettersDisplay.HighlightLetters(letterCollected, color);
            }

            // TODO Check if words completed => apply bonus / malus
        }

        private void AddWords(LettersDisplay[] lettersDisplays, WordDatabase wordDatabase)
        {
            foreach (LettersDisplay lettersDisplay in lettersDisplays)
            {
                if (lettersDisplay.IsEmpty() || lettersDisplay.IsComplete())
                {
                    print("Create word");
                    WordData randomWord = wordDatabase.GetRandomWordExcept(_currentWords);
                    lettersDisplay.SetWord(randomWord, letterCellPrefab);
                    _currentWords.Add(randomWord);
                }
            }

            OnActiveWordsChanged?.Invoke(_currentWords.ToArray());
        }

        // TODO - Check word complete => removeWord => AddWord
    }
}