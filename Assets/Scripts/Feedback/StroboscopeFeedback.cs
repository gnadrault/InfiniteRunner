using System.Collections;
using Gameplay.Segments;
using UnityEngine;
using Utils;

namespace Feedback
{
    /// <summary>
    /// Game effect stroboscope to the segments and obstacles
    /// </summary>
    public class StroboscopeFeedback : Feedback
    {
        
        private void OnEnable()
        {
            GameEvents.OnFeedbackStart += ApplyEffect;
            GameEvents.OnFeedbackEnd += ResetToAmbient;
        }

        private void OnDisable()
        {
            GameEvents.OnFeedbackStart -= ApplyEffect;
            GameEvents.OnFeedbackEnd -= ResetToAmbient;
        }

        protected override void ApplyEffect(FeedbackProfile profile)
        {
            if (!profile || !profile.Stroboscope.enabled) return;

            StopCurrent();
            routine = StartCoroutine(BlinkRoutine(profile.Stroboscope));
        }

        protected override void ResetToAmbient()
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
    }
}