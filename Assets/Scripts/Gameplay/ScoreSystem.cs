using System;
using Core;
using Data;
using TMPro;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class ScoreSystem : GameBehavior
    {
        [SerializeField] private TextMeshProUGUI scoreLabel;
        [SerializeField] private TextMeshProUGUI bestScoreLabel;

        private float _score;

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

        private void OnNewMeter(float _) => AddPoints(1);
        private void OnAddScorePoints(float point) => AddPoints(point);

        private void AddPoints(float points) => _score += points;

        private void RemovePercent(float percent) => _score = Mathf.RoundToInt(_score * (1 - percent));
        
        private void SaveScore()
        {
            bool newHighScore = ScoreSave.AddScore((int)_score);
            GameEvents.OnGameOver?.Invoke(new EndScoreData((int)_score, newHighScore));
        }
        
        protected override void GameplayUpdate()
        {
            scoreLabel.text = ((int)_score).ToString();
        }
    }
}