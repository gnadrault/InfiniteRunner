using Player;
using UnityEngine;

namespace World.GameElement.Virus.Solution
{
    public abstract class VirusSolution: MonoBehaviour
    {
        public abstract void OnAttached(PlayerController player);
        public abstract string GetLabel();
        public abstract void OnCheckSolution();
    }
}