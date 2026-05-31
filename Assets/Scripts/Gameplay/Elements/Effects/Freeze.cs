using Player;
using UnityEngine;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Freeze", menuName = "SyntaxError/Effects/Freeze")]
    public class Freeze : WordEffect
    {
        protected override bool IsBonus => true;
        
        protected override void OnApply()
        {
            player.ApplyFreeze();
        }

        protected override void OnRemove()
        {
            player.RemoveFreeze();
        }
    }
}
