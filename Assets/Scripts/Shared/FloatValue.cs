using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Shared
{
    /// <summary>
    /// Wrapper for float values, min-max or fixed value
    /// </summary>
    [Serializable]
    public class FloatValue
    {
        [SerializeField] private bool isRandom;
        [SerializeField] private float fixedValue = 1f;
        [SerializeField] private float min;
        [SerializeField] private float max;

        public float Value => isRandom ? Random.Range(min, max) : fixedValue;
    }
}