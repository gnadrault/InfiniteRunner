using Player;
using UnityEngine;

namespace Gameplay.Elements
{
    public abstract class Element : MonoBehaviour
    {
        public abstract void OnPlayerCollision(PlayerController player, Transform position);
    }
}