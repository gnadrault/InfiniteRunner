using System;
using System.Text;
using Data;
using Gameplay.Letters;
using Player;
using TMPro;
using UnityEngine;
using Utils;


namespace Gameplay
{
    public class DebugSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textPrefab;
        [SerializeField] private GameObject debugPanel;
        [SerializeField] private SegmentManager segmentManager;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private LettersSystem lettersSystem;

        private TextMeshProUGUI _timescaleTxt;
        private TextMeshProUGUI _speedTxt;
        private TextMeshProUGUI _phaseTxt;
        private TextMeshProUGUI _virusTxt;
        private TextMeshProUGUI _effectTxt;
        private TextMeshProUGUI _queueText;

        private void OnEnable()
        {
            GameEvents.OnNewPhase += HandleNewPhase;
        }
        
        private void OnDisable()
        {
            GameEvents.OnNewPhase -= HandleNewPhase;
        }

        private void HandleNewPhase(PhaseData phaseData)
        {
            _phaseTxt.text = "Phase: " + phaseData.name;
            _speedTxt.text = "Speed: " + phaseData.speed.ToString("F2");
        }

        private void Start()
        {
            _phaseTxt = Instantiate(textPrefab, debugPanel.transform);
            _speedTxt = Instantiate(textPrefab, debugPanel.transform);
            _timescaleTxt = Instantiate(textPrefab, debugPanel.transform);
            _virusTxt = Instantiate(textPrefab, debugPanel.transform);
            _effectTxt = Instantiate(textPrefab, debugPanel.transform);
            _queueText = Instantiate(textPrefab, debugPanel.transform);
        }

        private void Update()
        {
            _timescaleTxt.text = "Timescale: " + Time.timeScale.ToString("F2");
            _virusTxt.text = "Infected: " + playerController.IsPlayerInfected();
            _effectTxt.text = "Effect: " + lettersSystem.ActiveEffect?.name;
            StringBuilder builder = new();

            foreach (WordData item in lettersSystem.CompletedQueue)
            {
                builder.AppendLine(item.word + " - " + item.effect.name);
            }

            _queueText.text = builder.ToString();
        }
    }
}
