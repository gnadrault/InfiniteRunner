using UnityEngine;

namespace World
{
    public class Chunk : MonoBehaviour
    {
        public void Scroll(float speedFrame)
        {
            transform.Translate(Vector3.back * speedFrame);
        }
    }
}
