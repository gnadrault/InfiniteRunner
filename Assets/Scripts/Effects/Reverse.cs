using Player;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Reverse", menuName = "SyntaxError/Effects/Reverse")]
    public class Reverse : WordEffect
    {
        protected override bool IsBonus => false;
        
        protected override void OnApply()
        {
            PlayerController.Instance.ApplyInvert();
        }

        protected override void OnRemove()
        {
            PlayerController.Instance.RemoveInvert();
        }
    }
}
