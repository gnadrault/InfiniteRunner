using System.Collections;
using Player;
using UnityEngine;

namespace Gameplay.GameElement.Virus
{
    public class VirusBlue: VirusElement
    {
        [SerializeField] private int duration = 5;
        private PlayerController _player;
        
        public override void ApplyEffect(PlayerController player, Transform position)
        {
            _player = player;
            StartCoroutine(ApplyVirus());
            TimeScaleManager.Instance.SetTimeScale(timeReduce);
        }

        public override void RemoveEffect(PlayerController player)
        {
            TimeScaleManager.Instance.SetTimeScale(1f);
            Destroy(gameObject);
        }

        #region Solution

        private IEnumerator ApplyVirus()
        {
            yield return new WaitForSecondsRealtime(duration);
            _player.DetachVirus();
        }

        #endregion
    }
}