using Audio;
using Data;
using Player;
using UI;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Enemies
{
    /// <summary>
    /// Yellow virus game object
    /// Obstructed screen and add lag inputs
    /// </summary>
    public class VirusYellow: Virus
    {
        [Header("Settings")]
        [SerializeField] private float duration = 3f;
        [SerializeField] private float delay = 0.5f;
        
        private EffectTimer _timer;
        private bool _active;
        
        protected override void OnApply()
        {
            _timer = new EffectTimer(duration);
            _active = true;
            PlayerController.Instance.ApplyDelay(delay);
            GameEvents.OnGameFeelProfileStart?.Invoke(gameFeelProfile);
            AlertPanelUI.Instance.ShowPanel(AlertPanelType.Virus, alertTitleText, StringFormat.FormatTimer(duration));
            AudioManager.Instance.PlayOneShot(SfxType.AlertYellowVoice);
        }

        protected override void OnRemove()
        {
            _active = false;
            PlayerController.Instance.RemoveDelay();
            GameEvents.OnGameFeelEnd?.Invoke();
            AlertPanelUI.Instance.HideActivePanel();
            Destroy(gameObject);
        }
        
        protected override void GameplayUpdate()
        {
            if (!_active) return;
            _timer.Tick(Time.unscaledDeltaTime);
            AlertPanelUI.Instance.SetActionText(StringFormat.FormatTimer(_timer.Remaining));
            
            if (_timer.IsDone)
                PlayerController.Instance.DetachVirus();
        }
    }
}