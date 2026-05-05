using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace Gameplay.Letters
{
    public class LettersDisplay : MonoBehaviour
    {
        private WordData _currentWordData;
        private readonly List<LetterCell> _letterCells = new();

        public bool IsEmpty() => _letterCells.Count == 0;
        public bool IsComplete() => !_letterCells.Exists(letter => !letter.IsHighlighted);
        public WordData CurrentWordData => _currentWordData;

        public void SetWord(WordData wordData, LetterCell letterCellPrefab)
        {
            _currentWordData = wordData;
            
            _letterCells.ForEach(letterCell => Destroy(letterCell.gameObject)); // Destroy all old letters cells
            _letterCells.Clear();
            
            foreach (char letter in _currentWordData.word)
            {
                LetterCell letterCell = Instantiate(letterCellPrefab, transform.position, Quaternion.identity, transform);
                letterCell.Init(letter);
                _letterCells.Add(letterCell);
            }
        }
        
        public void HighlightLetters(string letterCollected, Color color)
        {
            foreach (LetterCell letterCell in _letterCells.Where(letterCell => !letterCell.IsHighlighted)) 
                letterCell.SetHighlight(letterCollected, color);
        }
    }
}