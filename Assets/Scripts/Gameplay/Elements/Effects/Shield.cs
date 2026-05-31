using Player;
using UI;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Shield", menuName = "SyntaxError/Effects/Shield")]
    public class Shield : WordEffect
    {
        protected override bool IsBonus => true;
        
        protected override void OnApply()
        {
            GameEvents.OnShieldBroken += RemoveEffect;
            player.ApplyShield();
        }

        protected override void OnRemove()
        {
            GameEvents.OnShieldBroken -= RemoveEffect;
            player.RemoveShield();
        }
    }
}
