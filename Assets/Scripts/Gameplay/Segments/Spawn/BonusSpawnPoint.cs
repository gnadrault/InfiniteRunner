using UnityEngine;

namespace Gameplay.Segments.Spawn
{
    /// <summary>
    /// Segment bonus spawn point to spawn bonus object on the current position
    /// </summary>
    public class BonusSpawnPoint : SpawnPoint
    {
        [SerializeField] private bool isElevated;

        public bool IsElevated => isElevated;
        
        private void OnValidate()
        {
            Vector3 pos = transform.localPosition;
            pos.y = isElevated ? ElevatedHeight : GroundHeight;
            transform.localPosition = pos;
        }
    }
}