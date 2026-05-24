using Player;
using UnityEngine;
using Utils;

namespace World.GameElement.WordEffect
{
    [CreateAssetMenu(fileName = "Spectral", menuName = "SyntaxError/Effects/Spectral")]
    public class Spectral : WordEffect
    {
        public override void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            base.ApplyEffect(player, runner);
            GameEvents.OnGhostBroken += OnEffectBroken;
            player.ApplyGhost();
            StartEffectTimer();
        }

        protected override void RemoveEffect()
        {
            if (isComplete) return;
            GameEvents.OnGhostBroken -= OnEffectBroken;
            player.RemoveGhost();
            base.RemoveEffect();
        }
    }
}
