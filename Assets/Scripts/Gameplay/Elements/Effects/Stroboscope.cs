using Feedback.Data;
using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Stroboscope", menuName = "SyntaxError/Effects/Stroboscope")]
    public class Stroboscope : WordEffect
    {
        [SerializeField] protected GameFeelProfile gameFeelProfile;

        protected override bool IsBonus => false;

        protected override void OnApply()
        {
            GameEvents.OnStroboscopeEffectStart?.Invoke(gameFeelProfile);
        }

        protected override void OnRemove()
        {
            GameEvents.OnStroboscopeEffectEnd?.Invoke();
        }
    }
}
