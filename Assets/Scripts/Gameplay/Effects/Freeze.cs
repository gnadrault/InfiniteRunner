using Player;
using UnityEngine;

namespace Gameplay.Effects
{
    [CreateAssetMenu(fileName = "Freeze", menuName = "SyntaxError/Effects/Freeze")]
    public class Freeze : WordEffect
    {
        public override bool IsBonus => false;
        
        protected override void OnApply()
        {
            PlayerController.Instance.ApplyFreeze();
        }

        protected override void OnRemove()
        {
            PlayerController.Instance.RemoveFreeze();
        }
    }
}
