using Player;
using UnityEngine;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Reverse", menuName = "SyntaxError/Effects/Reverse")]
    public class Reverse : WordEffect
    {
        protected override bool IsBonus => false;
        
        protected override void OnApply()
        {
            player.ApplyInvert();
        }

        protected override void OnRemove()
        {
            player.RemoveInvert();
        }
    }
}
