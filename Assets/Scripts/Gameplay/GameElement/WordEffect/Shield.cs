using Player;
using UnityEngine;
using Utils;

namespace Gameplay.GameElement.WordEffect
{
    [CreateAssetMenu(fileName = "Shield", menuName = "SyntaxError/Effects/Shield")]
    public class Shield : WordEffect
    {
        public override void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            base.ApplyEffect(playerController, runner);
            GameEvents.OnShieldBroken += OnEffectBroken;
            player.ApplyShield();
            StartEffectTimer();
        }

        public override void RemoveEffect()
        {
            if (isComplete) return;
            GameEvents.OnShieldBroken -= OnEffectBroken;
            player.RemoveShield();
            base.RemoveEffect();
        }
    }
}
