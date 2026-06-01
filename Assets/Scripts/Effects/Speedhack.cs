using Core;
using Gameplay;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Speedhack", menuName = "SyntaxError/Effects/Speedhack")]
    public class Speedhack : WordEffect
    {
        [SerializeField] private float speedTimeScale = 1.2f;

        public override bool IsBonus => false;
        
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
