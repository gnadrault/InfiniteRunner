using Core;
using UnityEngine;

namespace Gameplay.Effects
{
    [CreateAssetMenu(fileName = "Speedhack", menuName = "SyntaxError/Effects/Speedhack")]
    public class Speedhack : Effect
    {
        [Header("Settings")]
        [SerializeField] private float speedTimeScale = 1.2f;
        
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
