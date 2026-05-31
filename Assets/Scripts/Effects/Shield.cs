using Player;
using UnityEngine;
using Utils;

namespace Effects
{
    [CreateAssetMenu(fileName = "Shield", menuName = "SyntaxError/Effects/Shield")]
    public class Shield : WordEffect
    {
        protected override bool IsBonus => true;
        
        protected override void OnApply()
        {
            GameEvents.OnShieldBroken += RemoveEffect;
            PlayerController.Instance.ApplyShield();
        }

        protected override void OnRemove()
        {
            GameEvents.OnShieldBroken -= RemoveEffect;
            PlayerController.Instance.RemoveShield();
        }
    }
}
