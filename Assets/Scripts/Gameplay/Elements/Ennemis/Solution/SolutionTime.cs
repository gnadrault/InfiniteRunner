using System.Collections;
using Player;
using UnityEngine;

namespace Gameplay.Elements.Ennemis.Solution
{
    public class SolutionTime : VirusSolution
    {
        [SerializeField] private int duration = 5;
        private PlayerController _player;

        public override void OnAttached(PlayerController player)
        {
            _player = player;
            StartCoroutine(ApplyVirus());
        }

        private IEnumerator ApplyVirus()
        {
            yield return new WaitForSecondsRealtime(duration);
            _player.DetachVirus();
        }
    }
}