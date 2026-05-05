using Player;
using UnityEngine;

namespace World.GameElement
{
    public abstract class Element : MonoBehaviour
    {
        public abstract void OnPlayerCollision(PlayerController player);
    }
}