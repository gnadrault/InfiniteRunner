using Data;
using UI;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Enemies
{
    public class VirusYellow: Virus
    {
        [SerializeField] private float duration = 3f;
        [SerializeField] private float delay = 0.5f;
        
        private EffectTimer _timer;
        private bool _active;
        
        protected override void OnApply()
        {
            _timer = new EffectTimer(duration);
            _active = true;
            player.ApplyDelay(delay);
            GameEvents.OnGameFeelProfileStart?.Invoke(gameFeelProfile);
            AlertPanelUI.Instance.ShowPanel(AlertPanelType.Virus, alertTitleText, StringFormat.FormatTimer(duration));
        }

        protected override void OnRemove()
        {
            _active = false;
            player.RemoveDelay();
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
                player.DetachVirus();
        }
    }
}