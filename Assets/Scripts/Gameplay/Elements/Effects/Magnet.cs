using Player;
using UnityEngine;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Magnet", menuName = "SyntaxError/Effects/Magnet")]
    public class Magnet : WordEffect
    {
        public override void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            base.ApplyEffect(playerController, runner);
            player.ApplyMagnet();
            StartEffectTimer();
        }

        public override void RemoveEffect()
        {
            if (isComplete) return;
            player.RemoveMagnet();
            base.RemoveEffect();
        }
    }
}
