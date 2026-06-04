using Player;
using UnityEngine;

namespace Gameplay.Effects
{
    [CreateAssetMenu(fileName = "Magnet", menuName = "SyntaxError/Effects/Magnet")]
    public class Magnet : Effect
    {
        
        protected override void OnApply()
        {
            PlayerController.Instance.ApplyMagnet();
        }

        protected override void OnRemove()
        {
            PlayerController.Instance.RemoveMagnet();
        }
    }
}
