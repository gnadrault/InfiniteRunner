using Core;
using Data;
using Effects;
using UI;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class WordEffectRunner : GameBehavior, IEffectRunner
    {
        private WordEffect _activeEffect;
        private EffectTimer _activeTimer;

        public void Register(WordEffect wordEffect, float duration)
        {
            _activeEffect = wordEffect;
            _activeTimer = new EffectTimer(duration);
        }
        
        public void Stop()
        {
            if (!_activeEffect) return;
            _activeEffect.RemoveEffect();
            Clear();
        }

        protected override void GameplayUpdate()
        {
            if (_activeTimer == null) return;
            _activeTimer.Tick(Time.unscaledDeltaTime);
            AlertPanelUI.Instance.SetActionText(StringFormat.FormatTimer(_activeTimer.Remaining));

            if (_activeTimer.IsDone)
                Stop();
        }
        
        private void Clear()
        {
            _activeEffect = null;
            _activeTimer = null;
        }
    }
}