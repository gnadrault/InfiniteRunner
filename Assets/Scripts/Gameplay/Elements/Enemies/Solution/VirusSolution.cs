using Player;
using UnityEngine;

namespace Gameplay.Elements.Enemies.Solution
{
    public abstract class VirusSolution: MonoBehaviour
    {
        public abstract void OnAttached(PlayerController player);
    }
}