using TMPro;
using UnityEngine;
using Utils;

namespace Gameplay.Letters
{
    public class LetterCell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
            
        private bool _isHighlighted;
        private char _character;
        
        public bool IsHighlighted => _isHighlighted;
        public char Character => _character;
        
        private void OnValidate()
        {
            label = GetComponent<TextMeshProUGUI>();
        }
        
        public void Init(char letter)
        {
            _character = letter;
            label.text = letter.ToString();
        }

        public void SetHighlight(string letter, Color color)
        {
            _isHighlighted = letter == label.text;
            label.color = _isHighlighted ? color : Colors.Default;
        }
    }
}
