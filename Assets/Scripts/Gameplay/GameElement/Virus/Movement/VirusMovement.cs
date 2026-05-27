using Player;
using UnityEngine;

namespace Gameplay.GameElement.Virus.Movement
{
    public abstract class VirusMovement : MonoBehaviour
    {
        public abstract void OnAttached(PlayerController player);
    }
}