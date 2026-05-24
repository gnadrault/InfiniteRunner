using Gameplay;
using Player;
using UnityEngine;
using Utils;

namespace World.GameElement.WordEffect
{
    [CreateAssetMenu(fileName = "Slow", menuName = "SyntaxError/Effects/Slow")]
    public class Slow : WordEffect
    {
        [SerializeField] private float speedTimeScale = 0.5f;
        
        public override void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            base.ApplyEffect(player, runner);
            TimeScaleManager.Instance.SetTimeScale(speedTimeScale);
            StartEffectTimer();
        }

        protected override void RemoveEffect()
        {
            if (isComplete) return;
            TimeScaleManager.Instance.SetTimeScale(1f);
            base.RemoveEffect();
        }
    }
}
