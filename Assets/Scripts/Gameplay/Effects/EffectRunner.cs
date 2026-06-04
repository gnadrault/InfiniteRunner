using Core;
using UI;
using UnityEngine;
using Utils;

namespace Gameplay.Effects
{
    /// <summary>
    /// Runner to play, stop words effects (bonus, malus)
    /// </summary>
    public class EffectRunner : GameBehavior, IEffectRunner
    {
        private Effect _activeEffect;
        private EffectTimer _activeTimer;

        /// <summary>
        /// Register a new word effect to play
        /// </summary>
        /// <param name="effect"></param>
        /// <param name="duration"></param>
        public void Register(Effect effect, float duration)
        {
            _activeEffect = effect;
            _activeTimer = new EffectTimer(duration);
        }
        
        /// <summary>
        /// Stop the current active effect and remove it
        /// </summary>
        public void Stop()
        {
            if (!_activeEffect) return;
            _activeEffect.RemoveEffect();
            Clear();
        }

        /// <summary>
        /// Update the timer
        /// Check if timer is done => Stop the current effect
        /// </summary>
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