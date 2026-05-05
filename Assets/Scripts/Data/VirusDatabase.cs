using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using World.GameElement.Virus;

namespace Data
{
    [CreateAssetMenu(fileName = "VirusDatabase", menuName = "SyntaxError/VirusDatabase")]
    public class VirusDatabase : ScriptableObject
    {
        [SerializeField] private List<VirusElement> virus;

        public VirusElement GetPrefab()
        {
            return virus.First();
        }
    }
}