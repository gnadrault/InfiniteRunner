using Data;
using Player;
using UI;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Effects
{
    public abstract class WordEffect : ScriptableObject
    {
        [SerializeField] protected string effectName;
        [SerializeField] private float duration = 10f;
        protected abstract bool IsBonus { get; }
        public bool IsComplete { get; private set; }

        protected PlayerController player;

        private IEffectRunner Runner { get; set; }

        public void ApplyEffect(PlayerController playerController, IEffectRunner runner)
        {
            IsComplete = false;
            player = playerController;
            Runner = runner;
            Runner.Register(this, duration);
            OnApply();
            
            AlertPanelType alertPanel = IsBonus ? AlertPanelType.Bonus : AlertPanelType.Malus;
            AlertPanelUI.Instance.ShowPanel(alertPanel, name, StringFormat.FormatTimer(duration));
        }

        public void RemoveEffect()
        {
            IsComplete = true;
            OnRemove();
            AlertPanelUI.Instance.HideActivePanel();
        }

        protected abstract void OnApply();
        protected abstract void OnRemove();
    }
}