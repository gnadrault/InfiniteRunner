using UnityEngine;
using Utils;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Spectral", menuName = "SyntaxError/Effects/Spectral")]
    public class Spectral : WordEffect
    {
        protected override bool IsBonus => true;
        
        protected override void OnApply()
        {
            GameEvents.OnGhostBroken += RemoveEffect;
            player.ApplyGhost();
        }

        protected override void OnRemove()
        {
            GameEvents.OnGhostBroken -= RemoveEffect;
            player.RemoveGhost();
        }
    }
}
