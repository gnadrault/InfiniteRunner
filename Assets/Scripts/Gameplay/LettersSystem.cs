using System;
using System.Collections.Generic;
using Gameplay.Data;
using Player;
using UnityEngine;

namespace Gameplay
{
    public class LettersSystem : MonoBehaviour
    {
        [Header("Words")] 
        [SerializeField] private WordDatabase bonusWords;
        [SerializeField] private WordDatabase malusWords;
        [SerializeField] private LetterCell letterCellPrefab;
        [SerializeField] private LettersDisplay[] bonusWordsDisplays = new LettersDisplay[3];
        [SerializeField] private LettersDisplay[] malusWordsDisplays = new LettersDisplay[3];
        
        // TODO inheritance, bonus / malus

        private List<WordData> _currentWords = new List<WordData>();
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
            ActivateLetters(bonusWordsDisplays, letter);
            ActivateLetters(malusWordsDisplays, letter);
        }

        private void ActivateLetters(LettersDisplay[] lettersDisplays, string letterCollected)
        {
            foreach (LettersDisplay lettersDisplay in lettersDisplays)
            {
                lettersDisplay.HighlightLetters(letterCollected);
            }
            
            // TODO Check if words completed => apply bonus / malus
        }
        
        private void AddWords(LettersDisplay[] lettersDisplays, WordDatabase wordDatabase)
        {
            foreach (LettersDisplay lettersDisplay in lettersDisplays)
            {
                if (lettersDisplay.IsEmpty())
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
