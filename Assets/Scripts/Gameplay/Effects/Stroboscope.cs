using Feedback;
using UnityEngine;
using Utils;

namespace Gameplay.Effects
{
    [CreateAssetMenu(fileName = "Stroboscope", menuName = "SyntaxError/Effects/Stroboscope")]
    public class Stroboscope : WordEffect
    {
        [Header("Databases")]
        [SerializeField] protected FeedbackProfile feedbackProfile;

        public override bool IsBonus => false;

        protected override void OnApply()
        {
            GameEvents.OnFeedbackStart?.Invoke(feedbackProfile);
        }

        protected override void OnRemove()
        {
            GameEvents.OnFeedbackEnd?.Invoke();
        }
    }
}
