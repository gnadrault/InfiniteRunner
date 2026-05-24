using TMPro;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreLabel;

        private float _score;

        private void OnEnable()
        {
            GameEvents.OnAddScorePoints += AddPoints;
            GameEvents.OnRemovePercentPoints += RemovePercent;
        }

        private void OnDisable()
        {
            GameEvents.OnAddScorePoints -= AddPoints;
            GameEvents.OnRemovePercentPoints -= RemovePercent;
        }

        private void AddPoints(float points)  => _score += points;

        private void RemovePercent(float percent)
        {
            _score = Mathf.RoundToInt(_score * (1 - percent));
            print("Remove 1 percent");
        }
        

        private void Update()
        {
            scoreLabel.text = _score + "pts";
        }
    }
}
