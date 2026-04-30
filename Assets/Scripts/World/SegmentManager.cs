using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class SegmentManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int maxChunks = 5;
        [SerializeField] private float scrollSpeed = 2f;
        [SerializeField] private Segment segmentPrefab;
        [SerializeField] private Segment firstSegment;
        private List<Segment> _chunkList;
        
        private float _chunkLength;
        private float _chunkX;
        private float _chunkY;

        private void Start()
        {
            _chunkList = new List<Segment> { firstSegment };
            _chunkLength = firstSegment.GetComponent<Renderer>().bounds.size.z;
            _chunkX = firstSegment.transform.position.x;
            _chunkY = firstSegment.transform.position.y;
        }

        private void OnEnable()
        {
            Segment.OnChunkDestroyed += RemoveChunk;
        }

        private void OnDisable()
        {
            Segment.OnChunkDestroyed -= RemoveChunk;
        }

        private void AddChunk()
        {
            Segment lastSegment = _chunkList[_chunkList.Count - 1];
            Vector3 spawnPos = new Vector3(_chunkX, _chunkY, lastSegment.transform.position.z + _chunkLength);
            Segment newSegment = Instantiate(segmentPrefab, spawnPos, Quaternion.identity);
            _chunkList.Add(newSegment);
        }

        private void RemoveChunk(Segment segment)
        {
            _chunkList.Remove(segment);
        }

        private void Update()
        {
            while (_chunkList.Count < maxChunks)
            {
                AddChunk();
            }
            foreach (Segment chunk in _chunkList)
            {
                chunk.Scroll(scrollSpeed * Time.deltaTime);
            }
        }
    }
}
