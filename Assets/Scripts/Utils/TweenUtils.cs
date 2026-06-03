using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Utility class for transitions
    /// </summary>
    public static class TweenUtils
    {
        public static IEnumerator Transition(Action<float> onUpdate, float duration)
        {
            float time = 0f;
            while (time < 1f)
            {
                time += Time.deltaTime / duration;
                onUpdate(time);
                yield return null;
            }
            onUpdate(1f);
        }
        
        public static float EaseOutQuint(float t)
        {
            return 1f - Mathf.Pow(1 - t, 5);
        }

        public static float EaseInQuint(float t)
        {
            return t * t * t * t * t;
        }
        
        public static float EaseInQuad(float t)
        {
            return t * t * t * t;
        }
    }
}