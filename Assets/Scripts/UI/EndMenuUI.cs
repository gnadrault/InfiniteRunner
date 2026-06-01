using Audio;
using Core;
using Data;
using TMPro;
using UnityEngine;
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
            GameEvents.OnGameOver += OnGameOver;
            GameEvents.OnGameStateChanged += HandleState;
        }
        
        private void OnDisable()
        {
            GameEvents.OnGameOver -= OnGameOver;
            GameEvents.OnGameStateChanged -= HandleState;
        }

        private void OnGameOver(EndScoreData endScore)
        {
            scoreText.text = endScore.score.ToString();
            newHighScoreText.enabled = endScore.isNewHighScore;
            simpleScoreText.enabled = !endScore.isNewHighScore;
            AudioManager.Instance.PlayOneShot(endScore.isNewHighScore ? SfxType.BestHighScore : SfxType.GameOver);
            GameStateManager.Instance.SetState(GameState.GameOver);
        }
        
        private void HandleState(GameState state)
        {
            endCanvas.SetActive(state == GameState.GameOver);
        }
    }
}