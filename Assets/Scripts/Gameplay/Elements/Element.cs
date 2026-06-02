using Core;
using UnityEngine;

namespace Gameplay.Elements
{
    /// <summary>
    /// Abstract class for elements that can collide with the player
    /// </summary>
    public abstract class Element : GameBehavior
    {
        public abstract void OnPlayerCollision(Transform position);
    }
}