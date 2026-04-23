using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class ChunkManager : MonoBehaviour
    {
        [SerializeField] private int maxChunks = 5;
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
            print(_chunkLength);
        }

        private void AddChunk()
        {
            Chunk lastChunk = _chunkList[_chunkList.Count - 1];
            print(lastChunk.transform.position.z);
            Vector3 spawnPos = new Vector3(_chunkX, _chunkY, lastChunk.transform.position.z + _chunkLength);
            Chunk newChunk = Instantiate(chunkPrefab, spawnPos, Quaternion.identity);
            _chunkList.Add(newChunk);
        }

        private void Update()
        {
            while (_chunkList.Count < maxChunks)
            {
                AddChunk();
            }
        }
    }
}
