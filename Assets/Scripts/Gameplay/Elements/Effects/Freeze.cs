using Player;
using UnityEngine;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Freeze", menuName = "SyntaxError/Effects/Freeze")]
    public class Freeze : WordEffect
    {
        public override void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            base.ApplyEffect(playerController, runner);
            player.ApplyFreeze();
            StartEffectTimer();
        }

        public override void RemoveEffect()
        {
            if (isComplete) return;
            player.RemoveFreeze();
            base.RemoveEffect();
        }
    }
}
