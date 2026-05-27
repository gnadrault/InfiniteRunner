using Player;
using UnityEngine;

namespace Gameplay.Elements.Ennemis.Solution
{
    public abstract class VirusSolution: MonoBehaviour
    {
        public abstract void OnAttached(PlayerController player);
    }
}