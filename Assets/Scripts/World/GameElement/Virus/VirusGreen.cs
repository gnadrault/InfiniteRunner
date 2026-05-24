using System.Collections;
using Player;
using UI;
using UnityEngine;
using Utils;

namespace World.GameElement.Virus
{
    public class VirusGreen: VirusElement
    {
        [SerializeField] private float duration = 5f;
        private PlayerController _player;
        private float _currentTimer;
        private bool _attachedToPlayer;
        
        public override void ApplyEffect(PlayerController player, Transform position)
        {
            _player = player;
            _currentTimer = duration;
            TimeScaleManager.Instance.SetTimeScale(timeReduce);
            HUD.Instance.ShowVirusPanel(GetHUDLabel());
            StartCoroutine(ApplyVirus());
        }

        public override void RemoveEffect(PlayerController player)
        {
            TimeScaleManager.Instance.SetTimeScale(1f);
            HUD.Instance.HideVirusPanel();
            Destroy(gameObject);
        }

        #region Solution

        private IEnumerator ApplyVirus()
        {
            _attachedToPlayer = true;
            yield return new WaitForSeconds(duration);
            _attachedToPlayer = false;
            _player.DetachVirus();
        }

        #endregion
        
        private void Update()
        {
            if (!_attachedToPlayer) return;
            _currentTimer -= Time.deltaTime;
            _currentTimer = Mathf.Clamp(_currentTimer, 0f, duration);
            HUD.Instance.UpdateVirusLabel(GetHUDLabel());
        }

        private string GetHUDLabel()
        {
            int seconds = Mathf.FloorToInt(_currentTimer);
            int milliseconds = Mathf.FloorToInt((_currentTimer - seconds) * 100);
            return $"{seconds:D2}:{milliseconds:D2}";
        }
    }
}