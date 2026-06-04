using Player;
using UnityEngine;

namespace Gameplay.Effects
{
    [CreateAssetMenu(fileName = "Multiplier", menuName = "SyntaxError/Effects/Multiplier")]
    public class Multiplier : Effect
    {
        [Header("Settings")]
        [SerializeField] private float multiplierFactor = 2f;
        
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
