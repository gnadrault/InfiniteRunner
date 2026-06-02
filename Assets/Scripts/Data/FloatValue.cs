using System;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Wrapper for float values, min-max or fixed value
    /// </summary>
    [Serializable]
    public class FloatValue
    {
        [SerializeField] private float fixedValue;
        [SerializeField] private float min;
        [SerializeField] private float max;

        private float _value;

        public float Value => fixedValue != 0
            ? fixedValue
            : UnityEngine.Random.Range(min, max);
    }
}