using UnityEngine;

namespace Gameplay.Segments.Spawn
{
    /// <summary>
    /// Segment virus spawn point to spawn virus object on the current position
    /// </summary>
    public class VirusSpawnPoint : SpawnPoint
    {
        [SerializeField] private float spawnRate = 1f;
    }
}