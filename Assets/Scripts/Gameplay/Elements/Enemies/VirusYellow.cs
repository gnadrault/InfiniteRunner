using System.Collections;
using Player;
using UI;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Enemies
{
    public class VirusYellow: Virus
    {
        [SerializeField] private float duration = 5f;
        private PlayerController _player;
        private float _currentTimer;
        private bool _attachedToPlayer;
        
        public override void ApplyEffect(PlayerController player, Transform position)
        {
            _player = player;
            _currentTimer = duration;
            HUD.Instance.ShowVirusPanel(GetHUDLabel());
            GameEvents.OnGameFeelProfile?.Invoke(gameFeelProfile);
            StartCoroutine(ApplyVirus());
        }

        public override void RemoveEffect(PlayerController player)
        {
            HUD.Instance.HideVirusPanel();
            GameEvents.OnGameFeelEnd?.Invoke();
            Destroy(gameObject);
        }

        #region Solution

        private IEnumerator ApplyVirus()
        {
            _attachedToPlayer = true;
            yield return new WaitForSecondsRealtime(duration);
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