using Core;
using Data;
using UI;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Effects
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

        protected override void GameplayUpdate()
        {
            if (_activeTimer == null) return;
            _activeTimer.Tick(Time.unscaledDeltaTime);
            AlertPanelUI.Instance.SetActionText(StringFormat.FormatTimer(_activeTimer.Remaining));

            if (_activeTimer.IsDone)
            {
                _activeEffect.RemoveEffect();
                _activeTimer = null;
            }
        }
    }
}