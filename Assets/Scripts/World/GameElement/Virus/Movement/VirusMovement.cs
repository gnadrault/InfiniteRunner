using Player;
using UnityEngine;

namespace World.GameElement.Virus.Effect
{
    public abstract class VirusMovement : MonoBehaviour
    {
        public abstract void OnAttached(PlayerController player);
    }
}