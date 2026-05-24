using Data;
using Player;
using UnityEngine;

namespace World.GameElement.WordEffect
{
    public abstract class WordEffect: MonoBehaviour
    {
        public bool isDone;
        
        public virtual void ApplyEffect(PlayerController player)
        {
            // TODO : Display HUD effect bonus / malus, name, effect, progress bar
        }

        public virtual void RemoveEffect(PlayerController player)
        {
            // TODO : Hide HUD
            isDone = true;
        }
    }
}