using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class LetterCell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;


        private static readonly Color DefaultColor = Color.white;
        private static readonly Color HighlightColor = Color.cyan;
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

        public void SetHighlight(string letter)
        {
            _isHighlighted = letter == label.text;
            label.color = _isHighlighted ? HighlightColor : DefaultColor;
        }
    }
}
