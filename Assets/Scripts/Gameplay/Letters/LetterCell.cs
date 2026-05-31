using Core;
using TMPro;
using UnityEngine;

namespace Gameplay.Letters
{
    public class LetterCell : GameBehavior
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private TMP_FontAsset highlightFont;
        
        private bool _isHighlighted;
        public bool IsHighlighted => _isHighlighted;
        
        private void OnValidate()
        {
            label = GetComponent<TextMeshProUGUI>();
        }
        
        public void Init(char letter)
        {
            label.text = letter.ToString();
        }

        public void SetHighlight(string letter, Color color)
        {
            _isHighlighted = letter == label.text;
            if (_isHighlighted)
            {
                label.color = color;
                label.fontSize += 8;
                label.outlineWidth = 0.2f;
                label.outlineColor = color;
            }
        }
    }
}
