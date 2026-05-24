using Player;
using UnityEngine;
using Utils;

namespace World.GameElement.WordEffect
{
    [CreateAssetMenu(fileName = "Speedhack", menuName = "SyntaxError/Effects/Speedhack")]
    public class Speedhack : WordEffect
    {
        public override void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            base.ApplyEffect(player, runner);
            TimeScaleManager.Instance.SetTimeScale(1.2f);
            StartEffectTimer();
        }

        public override void RemoveEffect()
        {
            if (isComplete) return;
            TimeScaleManager.Instance.SetTimeScale(1f);
            base.RemoveEffect();
        }
    }
}
