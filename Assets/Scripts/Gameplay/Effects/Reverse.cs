using Player;
using UnityEngine;

namespace Gameplay.Effects
{
    [CreateAssetMenu(fileName = "Reverse", menuName = "SyntaxError/Effects/Reverse")]
    public class Reverse : Effect
    {
        
        protected override void OnApply()
        {
            PlayerController.Instance.ApplyInvert();
        }

        protected override void OnRemove()
        {
            PlayerController.Instance.RemoveInvert();
        }
    }
}
