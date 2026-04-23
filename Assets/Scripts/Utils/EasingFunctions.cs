using UnityEngine;

namespace Utils
{
    public static class EasingFunctions
    {
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