using System;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class ScoreSystem : MonoBehaviour
    {
    
        [SerializeField] private TextMeshProUGUI scoreLabel;
        
        public static event Action<int> ScoreChanged;

        private int _score;

        private void OnEnable()
        {
            ScoreChanged += UpdateScore;
        }

        private void OnDisable()
        {
            ScoreChanged -= UpdateScore;
        }

        private void UpdateScore(int point)
        {
            _score += point;
        }

        private void Update()
        {
            scoreLabel.text = _score + "pts";
        }

        public static void OnScoreChanged(int obj)
        {
            ScoreChanged?.Invoke(obj);
        }
    }
}
