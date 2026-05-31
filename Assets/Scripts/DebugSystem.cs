using System.Text;
using Core;
using Data;
using Gameplay;
using Player;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class DebugSystem : GameBehavior
{
    [SerializeField] private TextMeshProUGUI textPrefab;
    [SerializeField] private GameObject debugPanel;
    [SerializeField] private SegmentManager segmentManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private LettersSystem lettersSystem;

    private TextMeshProUGUI _timescaleTxt;
    private TextMeshProUGUI _speedTxt;
    private TextMeshProUGUI _phaseTxt;
    private TextMeshProUGUI _stateTxt;
    private TextMeshProUGUI _virusTxt;
    private TextMeshProUGUI _effectTxt;
    private TextMeshProUGUI _queueText;

    private void OnEnable()
    {
        GameEvents.OnNewPhase += HandleNewPhase;
        GameEvents.OnSpeedChanged += HandleSpeedChanged;
    }

    private void OnDisable()
    {
        GameEvents.OnNewPhase -= HandleNewPhase;
        GameEvents.OnSpeedChanged -= HandleSpeedChanged;
    }

    private void HandleNewPhase(PhaseData phaseData)
    {
        _phaseTxt.text = "Phase: " + phaseData.Name;
    }
        
    private void HandleSpeedChanged(float speed)
    {
        _speedTxt.text = "Speed: " + speed.ToString("F2");
    }

    private void Start()
    {
        _stateTxt = Instantiate(textPrefab, debugPanel.transform);
        _phaseTxt = Instantiate(textPrefab, debugPanel.transform);
        _speedTxt = Instantiate(textPrefab, debugPanel.transform);
        _timescaleTxt = Instantiate(textPrefab, debugPanel.transform);
        _virusTxt = Instantiate(textPrefab, debugPanel.transform);
        _effectTxt = Instantiate(textPrefab, debugPanel.transform);
        _queueText = Instantiate(textPrefab, debugPanel.transform);
    }

    protected override void AlwaysUpdate()
    {
        _stateTxt.text = GameStateManager.Instance.State.ToString();
        _timescaleTxt.text = "Timescale: " + Time.timeScale.ToString("F2");
        _virusTxt.text = "Infected: " + playerController.IsPlayerInfected();
        _effectTxt.text = "Effect: " + lettersSystem.ActiveEffect?.name;
        StringBuilder builder = new();

        foreach (WordData item in lettersSystem.CompletedQueue)
        {
            builder.AppendLine(item.Word + " - " + item.Effect.name);
        }

        _queueText.text = builder.ToString();
    }
}