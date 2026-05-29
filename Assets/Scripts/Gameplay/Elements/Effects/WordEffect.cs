using System.Collections;
using Player;
using UI;
using UnityEngine;

namespace Gameplay.Elements.Effects
{
    public abstract class WordEffect : ScriptableObject
    {
        [SerializeField] protected string effectName;
        [SerializeField] protected AlertHUD.PanelType panelType;
        [SerializeField] private float duration = 10f;
        [HideInInspector] public bool isComplete;
        
        protected PlayerController player;
        private Coroutine _timerCoroutine;

        private MonoBehaviour Runner { get; set; }

        public virtual void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            isComplete = false;
            player = playerController;
            Runner = runner;
        }

        public virtual void RemoveEffect()
        {
            isComplete = true;
            AlertHUD.Instance.ForceHidePanels();
        }

        protected void StartEffectTimer()
        {
            AlertHUD.Instance.ShowPanelTimed(panelType, effectName, duration);
            _timerCoroutine = Runner.StartCoroutine(EffectTimer());
        }

        private IEnumerator EffectTimer()
        {
            yield return new WaitForSeconds(duration);
            RemoveEffect();
        }
        
        protected void OnEffectBroken()
        {
            if (_timerCoroutine != null)
                Runner.StopCoroutine(_timerCoroutine);
            RemoveEffect();
        }
    }
}
