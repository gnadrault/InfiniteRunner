using Core;
using Data;
using TMPro;
using UnityEngine;

namespace UI
{
    
    /// <summary>
    /// Manage the leader board menu
    /// </summary>
    public class LeaderBoardUI : GameBehavior
    {
        [Header("Settings")]
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject scoreLinePrefab;
        
        /// <summary>
        /// Load saved scores add them to the scoresPanel
        /// </summary>
        private void Start()
        {
            ScoreData data = ScoreSave.Load();

            foreach (ScoreSaveData entry in data.entries)
            {
                GameObject row = Instantiate(scoreLinePrefab, parent);
                row.GetComponentsInChildren<TextMeshProUGUI>()[0].text = entry.date; 
                row.GetComponentsInChildren<TextMeshProUGUI>()[1].text = entry.score.ToString();
            }
        }
    }
}
