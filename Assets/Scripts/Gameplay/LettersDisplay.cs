using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Data;
using UnityEngine;

namespace Gameplay
{
    public class LettersDisplay: MonoBehaviour
    {
        private readonly List<LetterCell> _letterCells = new List<LetterCell>();

        public bool IsEmpty() => _letterCells.Count == 0;
        public bool IsComplete() => !_letterCells.Exists(letter => !letter.IsHighlighted);
        
        public void SetWord(WordData wordData, LetterCell letterCellPrefab)
        {
            _letterCells.Clear();
            char[] letters = wordData.word.ToCharArray();
            foreach (char letter in letters)
            {
                LetterCell letterCell = Instantiate(letterCellPrefab, transform.position, Quaternion.identity, transform);
                letterCell.Init(letter);
                _letterCells.Add(letterCell);
            }
        }
        
        public void HighlightLetters(string letterCollected)
        {
            foreach (LetterCell letterCell in _letterCells)
            {
                if (!letterCell.IsHighlighted)
                {
                    letterCell.SetHighlight(letterCollected);
                }
            }
        }
    }
}