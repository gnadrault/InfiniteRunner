using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Magnet", menuName = "SyntaxError/Effects/Magnet")]
    public class Magnet : WordEffect
    {
        protected override bool IsBonus => true;
        
        protected override void OnApply()
        {
            player.ApplyMagnet();
        }

        protected override void OnRemove()
        {
            player.RemoveMagnet();
        }
    }
}
