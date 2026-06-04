using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Effects
{
    [CreateAssetMenu(fileName = "Spectral", menuName = "SyntaxError/Effects/Spectral")]
    public class Spectral : Effect
    {
        
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
