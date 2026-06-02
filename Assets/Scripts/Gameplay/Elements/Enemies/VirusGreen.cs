using Audio;
using Data;
using Player;
using UI;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Enemies
{
    /// <summary>
    /// Green virus game object
    /// Remove percent on the score
    /// </summary>
    public class VirusGreen: Virus
    {
        [Header("Settings")]
        [SerializeField] private float duration = 5f;
        [SerializeField] private float percentScoreDamage = 1f;
        
        private EffectTimer _timer;
        private bool _active;
        private int _lastSecond;

        protected override void OnApply()
        {
            _timer = new EffectTimer(duration);
            _lastSecond = Mathf.CeilToInt(duration);
            _active = true;
            
            GameEvents.OnGameFeelProfileStart?.Invoke(gameFeelProfile);
            AlertPanelUI.Instance.ShowPanel(AlertPanelType.Virus, alertTitleText, StringFormat.FormatTimer(duration));
            AudioManager.Instance.PlayOneShot(SfxType.AlertGreenVoice);
        }
        
        protected override void OnRemove()
        {
            _active = false;
            GameEvents.OnGameFeelEnd?.Invoke();
            AlertPanelUI.Instance.HideActivePanel();
            Destroy(gameObject);
        }
        
        protected override void GameplayUpdate()
        {
            if (!_active) return;
            _timer.Tick(Time.unscaledDeltaTime);
            AlertPanelUI.Instance.SetActionText(StringFormat.FormatTimer(_timer.Remaining));

            int currentSecond = Mathf.CeilToInt(_timer.Remaining);
            if (currentSecond < _lastSecond)
            {
                _lastSecond = currentSecond;
                GameEvents.OnRemovePercentPoints?.Invoke(percentScoreDamage/100f);
            }
            
            if (_timer.IsDone) 
                PlayerController.Instance.DetachVirus();
        }
    }
}