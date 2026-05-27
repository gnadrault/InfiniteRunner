using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Spectral", menuName = "SyntaxError/Effects/Spectral")]
    public class Spectral : WordEffect
    {
        public override void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            base.ApplyEffect(playerController, runner);
            GameEvents.OnGhostBroken += OnEffectBroken;
            player.ApplyGhost();
            StartEffectTimer();
        }

        public override void RemoveEffect()
        {
            if (isComplete) return;
            GameEvents.OnGhostBroken -= OnEffectBroken;
            player.RemoveGhost();
            base.RemoveEffect();
        }
    }
}
