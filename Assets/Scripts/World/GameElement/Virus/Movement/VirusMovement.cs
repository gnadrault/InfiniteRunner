using Player;
using UnityEngine;

namespace World.GameElement.Virus.Movement
{
    public abstract class VirusMovement : MonoBehaviour
    {
        public abstract void OnAttached(PlayerController player);
    }
}