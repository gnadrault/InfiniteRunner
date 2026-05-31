using System;
using Core;
using Data;
using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace UI
{
    public class EndMenuUI : GameBehavior
    {
        [SerializeField] private TextMeshProUGUI newHighScoreText;
        [SerializeField] private TextMeshProUGUI simpleScoreText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject endCanvas;

        private void OnEnable()
        {
            GameEvents.OnEndMenu += EndGame;
        }
        
        private void OnDisable()
        {
            GameEvents.OnEndMenu -= EndGame;
        }

        private void EndGame(EndScoreData endScore)
        {
            TimeManager.Instance.SetTimeScale(0f);
            scoreText.text = endScore.score.ToString();
            newHighScoreText.enabled = endScore.isNewHighScore;
            simpleScoreText.enabled = !endScore.isNewHighScore;
            endCanvas.SetActive(true);
        }
    }
}