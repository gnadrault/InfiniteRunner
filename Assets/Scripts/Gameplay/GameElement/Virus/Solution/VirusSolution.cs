using Player;
using UnityEngine;

namespace Gameplay.GameElement.Virus.Solution
{
    public abstract class VirusSolution: MonoBehaviour
    {
        public abstract void OnAttached(PlayerController player);
    }
}