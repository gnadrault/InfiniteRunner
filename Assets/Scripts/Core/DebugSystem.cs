using Gameplay.Letters;
using Gameplay.Segments;

namespace Core
{
    using Player;
    using TMPro;
    using UnityEngine;

    public class DebugSystem : GameBehavior
    {
        [SerializeField] private TextMeshProUGUI textPrefab;
        [SerializeField] private GameObject debugPanel;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private LettersSystem lettersSystem;

        private TextMeshProUGUI _timescaleTxt;
        private TextMeshProUGUI _speedTxt;
        private TextMeshProUGUI _phaseTxt;
        private TextMeshProUGUI _stateTxt;
        private TextMeshProUGUI _virusTxt;
        private TextMeshProUGUI _effectTxt;

        private void Start()
        {
            _stateTxt = Instantiate(textPrefab, debugPanel.transform);
            _phaseTxt = Instantiate(textPrefab, debugPanel.transform);
            _speedTxt = Instantiate(textPrefab, debugPanel.transform);
            _timescaleTxt = Instantiate(textPrefab, debugPanel.transform);
            _virusTxt = Instantiate(textPrefab, debugPanel.transform);
            _effectTxt = Instantiate(textPrefab, debugPanel.transform);
        }

        protected override void AlwaysUpdate()
        {
            _stateTxt.text = GameStateManager.Instance.State.ToString();
            _timescaleTxt.text = "Timescale: " + Time.timeScale.ToString("F2");
            _phaseTxt.text = "Phase: " + SegmentManager.Instance.CurrentPhaseData.Name;
            _speedTxt.text = "Speed: " + SegmentManager.Instance.Speed.ToString("F2");
            _virusTxt.text = "Infected: " + playerController.IsPlayerInfected();
            _effectTxt.text = "Effect: " + lettersSystem.ActiveEffect?.name;
        }
    }
}