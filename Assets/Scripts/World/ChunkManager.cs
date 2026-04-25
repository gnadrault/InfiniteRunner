using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class ChunkManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int maxChunks = 5;
        [SerializeField] private float scrollSpeed = 2f;
        [SerializeField] private Chunk chunkPrefab;
        [SerializeField] private Chunk firstChunk;
        private List<Chunk> _chunkList;
        
        private float _chunkLength;
        private float _chunkX;
        private float _chunkY;

        private void Start()
        {
            _chunkList = new List<Chunk> { firstChunk };
            _chunkLength = firstChunk.GetComponent<Renderer>().bounds.size.z;
            _chunkX = firstChunk.transform.position.x;
            _chunkY = firstChunk.transform.position.y;
        }

        private void OnEnable()
        {
            Chunk.OnChunkDestroyed += RemoveChunk;
        }

        private void OnDisable()
        {
            Chunk.OnChunkDestroyed -= RemoveChunk;
        }

        private void AddChunk()
        {
            Chunk lastChunk = _chunkList[_chunkList.Count - 1];
            Vector3 spawnPos = new Vector3(_chunkX, _chunkY, lastChunk.transform.position.z + _chunkLength);
            Chunk newChunk = Instantiate(chunkPrefab, spawnPos, Quaternion.identity);
            _chunkList.Add(newChunk);
        }

        private void RemoveChunk(Chunk chunk)
        {
            _chunkList.Remove(chunk);
        }

        private void Update()
        {
            while (_chunkList.Count < maxChunks)
            {
                AddChunk();
            }
            foreach (Chunk chunk in _chunkList)
            {
                chunk.Scroll(scrollSpeed * Time.deltaTime);
            }
        }
    }
}
