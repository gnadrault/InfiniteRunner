using Player;
using UnityEngine;
using Utils;

namespace World.GameElement.WordEffect
{
    [CreateAssetMenu(fileName = "Slow", menuName = "SyntaxError/Effects/Slow")]
    public class Slow : WordEffect
    {
        public override void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            base.ApplyEffect(player, runner);
            TimeScaleManager.Instance.SetTimeScale(0.5f);
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
