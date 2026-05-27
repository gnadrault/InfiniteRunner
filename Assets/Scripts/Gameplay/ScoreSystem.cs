using TMPro;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreLabel;

        private float _score;
        
        private void OnNewMeter(float _) => AddPoints(1);
        private void OnLetterCollected(string _) => AddPoints(30);
        private void OnWordCompleted(int length) => AddPoints(length * 100);

        private void OnEnable()
        {
            GameEvents.OnNewMeter += OnNewMeter;
            GameEvents.OnLetterCollected += OnLetterCollected;
            GameEvents.OnWordCompleted += OnWordCompleted;
            GameEvents.OnRemovePercentPoints += RemovePercent;
        }

        private void OnDisable()
        {
            GameEvents.OnNewMeter -= OnNewMeter;
            GameEvents.OnLetterCollected -= OnLetterCollected;
            GameEvents.OnWordCompleted -= OnWordCompleted;
            GameEvents.OnRemovePercentPoints -= RemovePercent;
        }

        private void AddPoints(float points)  => _score += points;

        private void RemovePercent(float percent) => _score = Mathf.RoundToInt(_score * (1 - percent));
        

        private void Update()
        {
            scoreLabel.text = ((int)_score).ToString();
        }
    }
}
