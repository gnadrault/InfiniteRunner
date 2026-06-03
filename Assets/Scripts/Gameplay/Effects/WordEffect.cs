using Audio;
using Database;
using UI;
using UnityEngine;
using Utils;

namespace Gameplay.Effects
{
    /// <summary>
    /// Abstract scriptable objects for word effects
    /// </summary>
    public abstract class WordEffect : ScriptableObject
    {
        [Header("Settings")]
        [SerializeField] protected string effectName;
        [SerializeField] private float duration = 10f;
        public abstract bool IsBonus { get; }
        public bool IsComplete { get; private set; }
        private IEffectRunner Runner { get; set; }

        /// <summary>
        /// Apply the word effect
        /// </summary>
        /// <param name="runner"></param>
        public void ApplyEffect(IEffectRunner runner)
        {
            IsComplete = false;
            Runner = runner;
            Runner.Register(this, duration);
            OnApply();
            
            AlertPanelType alertPanel = IsBonus ? AlertPanelType.Bonus : AlertPanelType.Malus;
            AlertPanelUI.Instance.ShowPanel(alertPanel, name, StringFormat.FormatTimer(duration));
            AudioManager.Instance.PlayOneShot(IsBonus ? SfxType.BonusActivate : SfxType.MalusActivate);
        }

        /// <summary>
        /// Remove the word effect
        /// </summary>
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