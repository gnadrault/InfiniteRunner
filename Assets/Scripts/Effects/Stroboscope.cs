using Data;
using UnityEngine;
using Utils;

namespace Effects
{
    [CreateAssetMenu(fileName = "Stroboscope", menuName = "SyntaxError/Effects/Stroboscope")]
    public class Stroboscope : WordEffect
    {
        [Header("Databases")]
        [SerializeField] protected GameFeelProfile gameFeelProfile;

        public override bool IsBonus => false;

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
