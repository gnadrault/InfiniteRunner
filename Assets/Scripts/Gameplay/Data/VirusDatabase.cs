using UnityEngine;
using World.GameElement.Virus;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "VirusDatabase", menuName = "SyntaxError/VirusDatabase")]
    public class VirusDatabase : ScriptableObject
    {
        [SerializeField] private RedVirus redVirusPrefab;
        [SerializeField] private GreenVirus greenVirusPrefab;
        [SerializeField] private BlueVirus blueVirusPrefab;
        [SerializeField] private YellowVirus yellowVirusPrefab;

        public VirusElement GetPrefab(VirusElement virusElement)
        {
            return virusElement switch
            {
                RedVirus => redVirusPrefab,
                GreenVirus => greenVirusPrefab,
                BlueVirus => blueVirusPrefab,
                YellowVirus => yellowVirusPrefab,
                _ => null
            };
        }
    }
}