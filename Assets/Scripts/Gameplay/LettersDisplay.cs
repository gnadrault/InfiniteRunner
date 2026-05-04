using System.Collections.Generic;
using Gameplay.Data;
using UnityEngine;

namespace Gameplay
{
    public class LettersDisplay: MonoBehaviour
    {
        private List<LetterCell> _letterCells = new();

        public bool IsEmpty() => _letterCells.Count == 0;
        public bool IsComplete() => !_letterCells.Exists(letter => !letter.IsHighlighted);
        
        public void SetWord(WordData wordData, LetterCell letterCellPrefab)
        {
            _letterCells.ForEach(letterCell => Destroy(letterCell.gameObject)); // Destroy all letters cells
            _letterCells.Clear();
            char[] letters = wordData.word.ToCharArray();
            foreach (char letter in letters)
            {
                LetterCell letterCell = Instantiate(letterCellPrefab, transform.position, Quaternion.identity, transform);
                letterCell.Init(letter);
                _letterCells.Add(letterCell);
            }
        }
        
        
        public void HighlightLetters(string letterCollected, Color color)
        {
            foreach (LetterCell letterCell in _letterCells)
            {
                if (!letterCell.IsHighlighted)
                {
                    letterCell.SetHighlight(letterCollected, color);
                }
            }
        }

        public List<char> GetNonActiveLetters()
        {
            List<char> nonActiveLetters = new List<char>();
            foreach (var letterCell in _letterCells)
            {
                if (!letterCell.IsHighlighted)
                {
                    nonActiveLetters.Add(letterCell.Character);
                }
            }
            return nonActiveLetters;
        }
    }
}