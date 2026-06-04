using Audio;
using Database;
using Gameplay.Effects;
using Player;
using UI;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Enemies
{
    /// <summary>
    /// Blue virus game object
    /// Apply a random malus
    /// </summary>
    public class VirusBlue: Virus, IEffectRunner
    {
        [Header("Database")]
        [SerializeField] private WordEffectDatabase wordEffectDatabase;
        
        [Header("Settings")]
        [SerializeField] private int duration = 5;
        
        private Effect _activeEffect;
        private EffectTimer _activeTimer;
        
        protected override void OnApply()
        {
            WordEffect word = wordEffectDatabase.GetRandomWord(false);
            word.Effect.ApplyEffect(this);
            AudioManager.Instance.PlayOneShot(SfxType.AlertBlueVoice);
        }

        protected override void OnRemove()
        {
            Destroy(gameObject);
        }

        public void Register(Effect effect, float effectDuration)
        {
            _activeEffect = effect;
            _activeTimer = new EffectTimer(effectDuration);
        }

        public void Stop()
        {
            if (!_activeEffect) return;
            _activeEffect.RemoveEffect();
            _activeTimer = null;
            PlayerController.Instance.DetachVirus();
        }

        protected override void GameplayUpdate()
        {
            if (_activeTimer == null) return;
            _activeTimer.Tick(Time.unscaledDeltaTime);
            AlertPanelUI.Instance.SetActionText(StringFormat.FormatTimer(_activeTimer.Remaining));

            if (_activeTimer.IsDone)
                Stop();
        }
    }
}