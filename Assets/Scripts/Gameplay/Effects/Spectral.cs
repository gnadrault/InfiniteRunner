using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Effects
{
    [CreateAssetMenu(fileName = "Spectral", menuName = "SyntaxError/Effects/Spectral")]
    public class Spectral : WordEffect
    {
        public override bool IsBonus => true;
        
        protected override void OnApply()
        {
            GameEvents.OnGhostBroken += RemoveEffect;
            PlayerController.Instance.ApplyGhost();
        }

        protected override void OnRemove()
        {
            GameEvents.OnGhostBroken -= RemoveEffect;
            PlayerController.Instance.RemoveGhost();
        }
    }
}
