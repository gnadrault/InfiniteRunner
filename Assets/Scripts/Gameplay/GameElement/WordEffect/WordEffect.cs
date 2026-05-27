using System.Collections;
using Player;
using UnityEngine;

namespace Gameplay.GameElement.WordEffect
{
    public abstract class WordEffect : ScriptableObject
    {
        
        [SerializeField] private float duration = 10f;
        [HideInInspector] public bool isComplete;
        
        protected PlayerController player;
        private Coroutine timerCoroutine;

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
        }

        protected void StartEffectTimer()
        {
            timerCoroutine = Runner.StartCoroutine(EffectTimer());
        }

        private IEnumerator EffectTimer()
        {
            yield return new WaitForSecondsRealtime(duration);
            RemoveEffect();
        }
        
        protected void OnEffectBroken()
        {
            if (timerCoroutine != null)
                Runner.StopCoroutine(timerCoroutine);
            RemoveEffect();
        }
    }
}
