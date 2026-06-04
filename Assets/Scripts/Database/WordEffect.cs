using System;
using Gameplay.Effects;
using UnityEngine;

namespace Database
{
    [Serializable]
    public class WordEffect
    {
        [SerializeField] private string word;
        [SerializeField] private Effect effect;

        public string Word => word;
        public Effect Effect => effect;
    }
}