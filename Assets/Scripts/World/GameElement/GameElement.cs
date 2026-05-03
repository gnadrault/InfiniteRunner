using Player;
using UnityEngine;
using World.Spawn;

namespace World.GameElement
{
    public abstract class GameElement : MonoBehaviour
    {
        public virtual SpawnType SpawnType => SpawnType.Empty;
        public abstract void OnPlayerCollision(PlayerController player);
    }
}