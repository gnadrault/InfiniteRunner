using UnityEngine;
using World.GameElement.Virus;

namespace World.Spawn
{
    public class VirusSpawnPoint : SpawnPoint
    {
        [SerializeField] private VirusElement prefab;
        
        public VirusElement Spawn(Transform parent)
        {
            return Instantiate(prefab, transform.position, Quaternion.identity, parent);
        }
    }
}