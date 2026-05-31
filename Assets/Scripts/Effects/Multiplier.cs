using Player;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Multiplier", menuName = "SyntaxError/Effects/Multiplier")]
    public class Multiplier : WordEffect
    {
        [SerializeField] private float multiplierFactor = 2f;

        protected override bool IsBonus => true;
        
        protected override void OnApply()
        {
            PlayerController.Instance.ApplyMultiplier(multiplierFactor);
        }

        protected override void OnRemove()
        {
            PlayerController.Instance.RemoveMultiplier();
        }
    }
}
