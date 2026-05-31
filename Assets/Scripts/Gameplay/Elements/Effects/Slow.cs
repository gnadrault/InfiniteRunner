using Player;
using UI;
using UnityEngine;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Slow", menuName = "SyntaxError/Effects/Slow")]
    public class Slow : WordEffect
    {
        [SerializeField] private float speedTimeScale = 0.5f;

        protected override bool IsBonus => true;
        
        protected override void OnApply()
        {
            TimeScaleManager.Instance.SetTimeScale(speedTimeScale);
        }

        protected override void OnRemove()
        {
            TimeScaleManager.Instance.SetTimeScale(1f);
        }
    }
}
