using UnityEngine;

namespace World
{
    public class ChunkDestoy : MonoBehaviour
    {
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Chunk chunk))
            {
                Destroy(chunk.gameObject);
            }
        }
    }
}
