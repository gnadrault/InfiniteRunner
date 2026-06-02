using System.Collections;
using Core;
using Data;
using Data.Database;
using Gameplay;
using Gameplay.Segments;
using UnityEngine;
using Utils;

namespace Feedback
{
    /// <summary>
    /// Game effect stroboscope to the segments and obstacles
    /// </summary>
    public class StroboscopeEffect : GameBehavior, IGameFeelEffect
    {
        private Coroutine _blinkRoutine;
        
        private void OnEnable()
        {
            GameEvents.OnStroboscopeEffectStart += ApplyEffect;
            GameEvents.OnStroboscopeEffectEnd += ResetToAmbient;
        }

        private void OnDisable()
        {
            GameEvents.OnStroboscopeEffectStart -= ApplyEffect;
            GameEvents.OnStroboscopeEffectEnd -= ResetToAmbient;
        }

        public void ApplyEffect(GameFeelProfile profile)
        {
            if (!profile || !profile.Stroboscope.enabled) return;

            StopCurrent();
            _blinkRoutine = StartCoroutine(BlinkRoutine(profile.Stroboscope));
        }
        
        public void ResetToAmbient()
        {
            StopCurrent();
            SetAllSegments(true);
        }
        
        private IEnumerator BlinkRoutine(StroboscopeSection data)
        {
            while (true)
            {
                SetAllSegments(true);
                yield return new WaitForSeconds(data.visibleTime);
                SetAllSegments(false);
                yield return new WaitForSeconds(data.invisibleTime);
            }
        }
        
        private void SetAllSegments(bool visible)
        {
            foreach (Segment segment in SegmentManager.Instance.ActiveSegments)
                segment.ToggleBlink(visible);
        }

        private void StopCurrent()
        {
            if (_blinkRoutine == null) return;
            StopCoroutine(_blinkRoutine);
            _blinkRoutine = null;
        }
    }
}