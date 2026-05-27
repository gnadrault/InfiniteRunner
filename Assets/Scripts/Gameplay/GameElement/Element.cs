using Player;
using UnityEngine;

namespace Gameplay.GameElement
{
    public abstract class Element : MonoBehaviour
    {
        public abstract void OnPlayerCollision(PlayerController player, Transform position);
    }
}