using Player;
using UI;
using UnityEngine;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Slow", menuName = "SyntaxError/Effects/Slow")]
    public class Slow : WordEffect
    {
        [SerializeField] private float speedTimeScale = 0.5f;
        
        public override void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            base.ApplyEffect(playerController, runner);
            TimeScaleManager.Instance.SetTimeScale(speedTimeScale);
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
