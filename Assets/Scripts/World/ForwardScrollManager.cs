using System;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class ForwardScrollManager : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;

        public void HandleScrollChunks(List<Chunk> chunks)
        {
            foreach (Chunk chunk in chunks)
            {
                chunk.Scroll(speed * Time.deltaTime);
            }
        }
    }
}
