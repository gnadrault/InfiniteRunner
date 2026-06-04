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
    public abstract class Effect : ScriptableObject
    {
        [Header("Parameters")]
        [SerializeField] private string effectName;
        [SerializeField] private bool isBonus;
        [SerializeField] private float duration = 10f;
        
        public bool IsComplete { get; private set; }

        public bool IsBonus => isBonus;

        /// <summary>
        /// Apply the word effect
        /// </summary>
        /// <param name="runner"></param>
        public void ApplyEffect(IEffectRunner runner)
        {
            IsComplete = false;
            runner.Register(this, duration);
            OnApply();
            
            AlertPanelType alertPanel = isBonus ? AlertPanelType.Bonus : AlertPanelType.Malus;
            AlertPanelUI.Instance.ShowPanel(alertPanel, name, StringFormat.FormatTimer(duration));
            AudioManager.Instance.PlayOneShot(isBonus ? SfxType.BonusActivate : SfxType.MalusActivate);
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