using Player;
using UnityEngine;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Multiplier", menuName = "SyntaxError/Effects/Multiplier")]
    public class Multiplier : WordEffect
    {
        [SerializeField] private float multiplierFactor = 2f;
        
        public override void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            base.ApplyEffect(playerController, runner);
            player.ApplyMultiplier(multiplierFactor);
            StartEffectTimer();
        }

        public override void RemoveEffect()
        {
            if (isComplete) return;
            player.RemoveMultiplier();
            base.RemoveEffect();
        }
    }
}
