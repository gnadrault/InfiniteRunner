using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Effects
{
    [CreateAssetMenu(fileName = "Shield", menuName = "SyntaxError/Effects/Shield")]
    public class Shield : Effect
    {
        
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
