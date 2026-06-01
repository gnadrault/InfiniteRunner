using Core;
using Gameplay;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Slow", menuName = "SyntaxError/Effects/Slow")]
    public class Slow : WordEffect
    {
        [SerializeField] private float speedTimeScale = 0.5f;

        public override bool IsBonus => true;
        
        protected override void OnApply()
        {
            TimeManager.Instance.SetGameplayTimeScale(speedTimeScale);
        }

        protected override void OnRemove()
        {
            TimeManager.Instance.SetGameplayTimeScale(1f);
        }
    }
}
