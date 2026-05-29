using Player;
using UnityEngine;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Reverse", menuName = "SyntaxError/Effects/Reverse")]
    public class Reverse : WordEffect
    {
        public override void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            base.ApplyEffect(playerController, runner);
            player.ApplyInvert();
            StartEffectTimer();
        }

        public override void RemoveEffect()
        {
            if (isComplete) return;
            player.RemoveInvert();
            base.RemoveEffect();
        }
    }
}
