using UnityEngine;
using World.Data;

namespace World.GameElement
{
    public abstract class GameElement : MonoBehaviour
    {
        public virtual SpawnType SpawnType => SpawnType.Empty;
    }
}