using Core;
using UnityEngine;
using Utils;

namespace Feedback
{
    /// <summary>
    /// Abstract feedback effect
    /// </summary>
    public abstract class Feedback : GameBehavior
    {
        protected Coroutine routine;
        
        private void OnEnable()
        {
            GameEvents.OnFeedbackStart += ApplyEffect;
            GameEvents.OnFeedbackEnd += ResetToAmbient;
        }

        private void OnDisable()
        {
            GameEvents.OnFeedbackStart -= ApplyEffect;
            GameEvents.OnFeedbackEnd -= ResetToAmbient;
        }

        protected abstract void ApplyEffect(FeedbackProfile profile);
        protected abstract void ResetToAmbient();
        
        protected void StopCurrent()
        {
            if (routine == null) return;
            StopCoroutine(routine);
            routine = null;
        }
    }
}