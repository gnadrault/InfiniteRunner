using System.Collections;
using Player;
using UnityEngine;

namespace World.GameElement.WordEffect
{
    public abstract class WordEffect : ScriptableObject
    {
        
        [SerializeField] private float duration = 10f;
        [HideInInspector] public bool isComplete;
        
        protected PlayerController player;
        protected Coroutine timerCoroutine;

        protected MonoBehaviour Runner { get; private set; }

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
        
        protected IEnumerator EffectTimer()
        {
            yield return new WaitForSeconds(duration);
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
