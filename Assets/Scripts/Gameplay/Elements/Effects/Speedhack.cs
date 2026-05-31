using Player;
using UI;
using UnityEngine;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Speedhack", menuName = "SyntaxError/Effects/Speedhack")]
    public class Speedhack : WordEffect
    {
        [SerializeField] private float speedTimeScale = 1.2f;

        protected override bool IsBonus => false;
        
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
