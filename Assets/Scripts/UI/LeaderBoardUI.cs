using Core;
using Data;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LeaderBoardUI : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject scoreLinePrefab;
        
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
