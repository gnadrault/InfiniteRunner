using Audio;
using Data;
using Effects;
using Player;
using UI;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Enemies
{
    public class VirusBlue: Virus, IEffectRunner
    {
        [SerializeField] private int duration = 5;
        [SerializeField] private WordDatabase wordsDatabase;
        
        private WordEffect _activeEffect;
        private EffectTimer _activeTimer;
        
        protected override void OnApply()
        {
            WordData word = wordsDatabase.GetRandomWord(false);
            word.Effect.ApplyEffect(this);
            AudioManager.Instance.PlayOneShot(SfxType.AlertBlueVoice);
        }

        protected override void OnRemove()
        {
            Destroy(gameObject);
        }

        public void Register(WordEffect wordEffect, float effectDuration)
        {
            _activeEffect = wordEffect;
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