using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class LetterCell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI character;

        private static readonly Color DefaultColor = Color.white;
        private static readonly Color HighlightColor = Color.cyan;
        private bool _isHighlighted;
        
        public bool IsHighlighted => _isHighlighted;
        
        private void OnValidate()
        {
            character = GetComponent<TextMeshProUGUI>();
        }
        
        public void Init(char letter)
        {
            character.text = letter.ToString();
        }

        public void SetHighlight(string letter)
        {
            _isHighlighted = letter == character.text;
            character.color = _isHighlighted ? HighlightColor : DefaultColor;
        }
    }
}
