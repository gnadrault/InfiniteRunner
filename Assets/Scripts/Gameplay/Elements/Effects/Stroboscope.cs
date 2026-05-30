using Feedback.Data;
using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Effects
{
    [CreateAssetMenu(fileName = "Stroboscope", menuName = "SyntaxError/Effects/Stroboscope")]
    public class Stroboscope : WordEffect
    {
        [SerializeField] protected GameFeelProfile gameFeelProfile;
        
        public override void ApplyEffect(PlayerController playerController, MonoBehaviour runner)
        {
            base.ApplyEffect(playerController, runner);
            GameEvents.OnStroboscopeEffectStart?.Invoke(gameFeelProfile);
            StartEffectTimer();
        }

        public override void RemoveEffect()
        {
            if (isComplete) return;
            GameEvents.OnStroboscopeEffectEnd?.Invoke();
            base.RemoveEffect();
        }
    }
}
