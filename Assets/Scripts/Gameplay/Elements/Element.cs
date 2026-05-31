using Core;
using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Elements
{
    public abstract class Element : GameBehavior
    {
        public abstract void OnPlayerCollision(PlayerController player, Transform position);
    }
}