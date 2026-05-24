using Player;
using UnityEngine;
using Utils;

namespace World.GameElement.WordEffect
{
    [CreateAssetMenu(fileName = "Shield", menuName = "SyntaxError/Effects/Shield")]
    public class Shield : WordEffect
    {
        public override void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            base.ApplyEffect(player, runner);
            GameEvents.OnShieldBroken += OnEffectBroken;
            player.ApplyShield();
            StartEffectTimer();
        }

        protected override void RemoveEffect()
        {
            if (isComplete) return;
            GameEvents.OnShieldBroken -= OnEffectBroken;
            player.RemoveShield();
            base.RemoveEffect();
        }
    }
}
