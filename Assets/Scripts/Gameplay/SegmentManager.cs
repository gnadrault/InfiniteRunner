using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using Feedback.Data;
using Gameplay.Segments;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class SegmentManager : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private int numSegments = 7;

        [Header("Segments")] 
        [SerializeField] private Segment firstSegment;
        [SerializeField] private int segmentsCountPerPhase = 5;

        private float _segmentLength;
        private float _segmentX;
        private float _segmentY;

        private readonly List<Segment> _activeSegmentList = new();
        private readonly List<Segment> _poolSegmentList = new();
        private SegmentBuilder _segmentBuilder;
        private PhaseData _currentPhaseData;
        private float _speed;
        private Coroutine _blinkRoutine;

        private void Awake()
        {
            _segmentBuilder = GetComponent<SegmentBuilder>();
        }

        private void Start()
        {
            _activeSegmentList.Add(firstSegment);
            _segmentLength = firstSegment.GetComponentInChildren<Renderer>().bounds.size.z;
            _segmentX = firstSegment.transform.position.x;
            _segmentY = firstSegment.transform.position.y;
        }

        private void OnEnable()
        {
            GameEvents.OnSegmentDestroyed += RemoveSegment;
            GameEvents.OnPlayerDied += StopScroll;
            GameEvents.OnNewPhase += HandleNewPhase;
            GameEvents.OnStroboscopeEffectStart += HandleStartStroboscope;
            GameEvents.OnStroboscopeEffectEnd += HandleEndStroboscope;
        }

        private void OnDisable()
        {
            GameEvents.OnSegmentDestroyed -= RemoveSegment;
            GameEvents.OnPlayerDied -= StopScroll;
            GameEvents.OnNewPhase -= HandleNewPhase;
            GameEvents.OnStroboscopeEffectStart -= HandleStartStroboscope;
            GameEvents.OnStroboscopeEffectEnd -= HandleEndStroboscope;
        }
        
        private void HandleStartStroboscope(GameFeelProfile profile)
        {
            if (!profile || !profile.Stroboscope.enabled) return;
            StroboscopeSection data = profile.Stroboscope;
            
            StopCurrent();
            _blinkRoutine = StartCoroutine(BlinkRoutine(data));
        }
        
        private void HandleEndStroboscope()
        {
            ResetToAmbient();
        }

        private IEnumerator BlinkRoutine(StroboscopeSection data)
        {
            while (true)
            {
                yield return BlinkSegments(true, data.visibleTime);
                yield return BlinkSegments(false, data.invisibleTime);
            }
        }
        
        private IEnumerator BlinkSegments(bool active, float time)
        {
            foreach (Segment segment in _activeSegmentList)
                segment.ToggleBlink(active);
                
            yield return new WaitForSeconds(time);
        }
        
        public void ResetToAmbient()
        {
            StopCurrent();
            foreach (Segment segment in _activeSegmentList)
                segment.ToggleBlink(true);
        }

        private void StopCurrent()
        {
            if (_blinkRoutine == null) return;
            StopCoroutine(_blinkRoutine);
            _blinkRoutine = null;
        }

        private void AddSegment()
        {
            Segment lastSegment = _activeSegmentList[^1]; // Prevent to pull same segment twice
            Vector3 spawnPosition =
                new Vector3(_segmentX, _segmentY, lastSegment.transform.position.z + _segmentLength);

            // Pool segment
            Segment pooledSegment = GetNextSegment(lastSegment);
            Segment newSegment = Instantiate(pooledSegment, spawnPosition, Quaternion.identity);

            // Segment builder
            _segmentBuilder.GenerateSegmentObjects(newSegment, _currentPhaseData);
            _activeSegmentList.Add(newSegment);
        }

        private Segment GetNextSegment(Segment lastSegment)
        {
            List<Segment> segmentsPool = _poolSegmentList.Where(s => s != lastSegment).ToList();
            return segmentsPool[Random.Range(0, segmentsPool.Count)];
        }

        private void RemoveSegment(Segment segment)
        {
            _activeSegmentList.Remove(segment);
        }

        private void StopScroll()
        {
            _speed = 0f;
        }

        private void HandleNewPhase(PhaseData phaseData)
        {
            _currentPhaseData = phaseData;
            _speed = phaseData.Speed;
            GameEvents.OnSpeedChanged?.Invoke(_speed);
            _poolSegmentList.AddRange(phaseData.NewSegments);
        }

        private void Update()
        {
            while (_activeSegmentList.Count < numSegments)
                AddSegment();

            foreach (Segment segment in _activeSegmentList)
                segment.Scroll(_speed * Time.deltaTime);
        }
    }
}