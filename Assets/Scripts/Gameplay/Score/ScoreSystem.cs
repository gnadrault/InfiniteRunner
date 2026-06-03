using Core;
using TMPro;
using UnityEngine;
using Utils;

namespace Gameplay.Score
{
    /// <summary>
    /// Manage the player score
    /// </summary>
    public class ScoreSystem : GameBehavior
    {
        [SerializeField] private TextMeshProUGUI scoreLabel;
        [SerializeField] private TextMeshProUGUI bestScoreLabel;

        private float _score;

        /// <summary>
        /// Load and display the highest score
        /// No score => the text field is empty
        /// </summary>
        private void Start()
        {
            ScoreData data = ScoreSave.Load();
            if (data != null && data.entries.Count > 0)
                bestScoreLabel.text = $"High score : {ScoreSave.Load().entries[0].score.ToString()}";
            else
                bestScoreLabel.text = "";
        }

        private void OnEnable()
        {
            GameEvents.OnNewMeter += OnNewMeter;
            GameEvents.OnAddScorePoints += OnAddScorePoints;
            GameEvents.OnRemovePercentPoints += RemovePercent;
            GameEvents.OnEndGame += SaveScore;
        }

        private void OnDisable()
        {
            GameEvents.OnNewMeter -= OnNewMeter;
            GameEvents.OnAddScorePoints -= OnAddScorePoints;
            GameEvents.OnRemovePercentPoints -= RemovePercent;
            GameEvents.OnEndGame -= SaveScore;
        }

        private void OnNewMeter(float _) => AddPoints(1); // On new distinct meter => add point
        private void OnAddScorePoints(float point) => AddPoints(point);

        private void AddPoints(float points) => _score += points; // Add points to the current score

        private void RemovePercent(float percent)
        {
            _score = Mathf.Max(0, Mathf.RoundToInt(_score * (1 - percent))); // Remove the score percent to the current score
        }
        
        /// <summary>
        /// Save the current score
        /// </summary>
        private void SaveScore()
        {
            bool newHighScore = ScoreSave.AddScore((int)_score);
            GameEvents.OnGameOver?.Invoke(new EndScoreData((int)_score, newHighScore));
        }
        
        protected override void GameplayUpdate()
        {
            scoreLabel.text = ((int)_score).ToString(); // Display the current score display
        }
    }
}