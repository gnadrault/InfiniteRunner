using System.Collections.Generic;
using Gameplay.GameElement.Virus;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "VirusDatabase", menuName = "SyntaxError/VirusDatabase")]
    public class VirusDatabase : ScriptableObject
    {
        [SerializeField] private List<VirusElement> virus;
        [SerializeField] private float spawnRate = 1f;

        public VirusElement GetPrefab()
        {
            return virus[Random.Range(0, virus.Count)];
        }
    }
}