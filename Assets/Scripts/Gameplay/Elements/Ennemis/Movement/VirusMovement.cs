using Player;
using UnityEngine;

namespace Gameplay.Elements.Ennemis.Movement
{
    public abstract class VirusMovement : MonoBehaviour
    {
        public abstract void OnAttached(PlayerController player);
    }
}