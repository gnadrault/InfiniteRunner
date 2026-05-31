using Player;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Magnet", menuName = "SyntaxError/Effects/Magnet")]
    public class Magnet : WordEffect
    {
        protected override bool IsBonus => true;
        
        protected override void OnApply()
        {
            PlayerController.Instance.ApplyMagnet();
        }

        protected override void OnRemove()
        {
            PlayerController.Instance.RemoveMagnet();
        }
    }
}
