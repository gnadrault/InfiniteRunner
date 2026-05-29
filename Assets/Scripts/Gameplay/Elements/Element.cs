using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Elements
{
    public abstract class Element : MonoBehaviour
    {
        public abstract void OnPlayerCollision(PlayerController player, Transform position);
    }
}