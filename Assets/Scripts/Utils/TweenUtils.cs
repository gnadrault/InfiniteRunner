using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
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
            return 1f - (t * t * t * t * t);
        }
    }
}