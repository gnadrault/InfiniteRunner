using Data;
using Gameplay.Elements.Effects;
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
        private EffectTimer _timer;
        
        protected override void OnApply()
        {
            WordData word = wordsDatabase.GetRandomWord(false);
            word.Effect.ApplyEffect(player, this);
        }

        protected override void OnRemove()
        {
            Destroy(gameObject);
        }

        public void Register(WordEffect wordEffect, float effectDuration)
        {
            _activeEffect = wordEffect;
            _timer = new EffectTimer(effectDuration);
        }

        private void Update()
        {
            if (_timer == null) return;
            _timer.Tick(Time.unscaledDeltaTime);
            AlertPanelUI.Instance.SetActionText(StringFormat.FormatTimer(_timer.Remaining));

            if (_timer.IsDone)
            {
                _activeEffect.RemoveEffect();
                player.DetachVirus();
                _timer = null;
            }
        }
    }
}