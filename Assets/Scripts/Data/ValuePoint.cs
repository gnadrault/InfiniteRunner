using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class ValuePoint
    {
        private enum Mode { Fixed, Random }

        [SerializeField] private Mode mode;
        [SerializeField] private int fixedValue;
        [SerializeField] private int min;
        [SerializeField] private int max;

        public int Value => mode == Mode.Fixed
            ? fixedValue
            : UnityEngine.Random.Range(min, max);
    }
}