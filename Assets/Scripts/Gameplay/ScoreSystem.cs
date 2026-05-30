using System;
using Core;
using Data;
using TMPro;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreLabel;
        [SerializeField] private TextMeshProUGUI bestScoreLabel;

        private float _score;

        private void Start()
        {
            bestScoreLabel.text = $"High score : {ScoreSave.Load().entries[0].score.ToString()}";
        }

        private void OnEnable()
        {
            GameEvents.OnNewMeter += OnNewMeter;
            GameEvents.OnLetterCollected += OnLetterCollected;
            GameEvents.OnWordCompleted += OnWordCompleted;
            GameEvents.OnRemovePercentPoints += RemovePercent;
            GameEvents.OnEndGame += SaveScore;
        }

        private void OnDisable()
        {
            GameEvents.OnNewMeter -= OnNewMeter;
            GameEvents.OnLetterCollected -= OnLetterCollected;
            GameEvents.OnWordCompleted -= OnWordCompleted;
            GameEvents.OnRemovePercentPoints -= RemovePercent;
            GameEvents.OnEndGame -= SaveScore;
        }
        
        private void OnNewMeter(float _) => AddPoints(1);
        private void OnLetterCollected(string _, bool multiplier) => AddPoints(30 * (multiplier ? 2 : 1));
        private void OnWordCompleted(int length) => AddPoints(length * 100);

        private void AddPoints(float points)  => _score += points;

        private void RemovePercent(float percent) => _score = Mathf.RoundToInt(_score * (1 - percent));


        private void SaveScore()
        {
            bool newHighScore = ScoreSave.AddScore((int)_score);
            GameEvents.OnEndMenu?.Invoke(new EndScoreData((int)_score, newHighScore));
        }
        

        private void Update()
        {
            scoreLabel.text = ((int)_score).ToString();
        }
    }
}
