using Core;
using UnityEngine;

namespace Gameplay.Effects
{
    [CreateAssetMenu(fileName = "Slow", menuName = "SyntaxError/Effects/Slow")]
    public class Slow : Effect
    {
        [Header("Settings")]
        [SerializeField] private float speedTimeScale = 0.5f;
        
        protected override void OnApply()
        {
            TimeManager.Instance.SetGameplayTimeScale(speedTimeScale);
        }

        protected override void OnRemove()
        {
            TimeManager.Instance.SetGameplayTimeScale(1f);
        }
    }
}
