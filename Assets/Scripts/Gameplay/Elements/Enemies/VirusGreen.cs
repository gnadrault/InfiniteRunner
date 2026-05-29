using System.Collections;
using Player;
using UI;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Enemies
{
    public class VirusGreen: Virus
    {
        [SerializeField] private float duration = 5f;
        private PlayerController _player;
        private float _currentTimer;
        private bool _attachedToPlayer;
        
        public override void ApplyEffect(PlayerController player, Transform position)
        {
            _player = player;
            _currentTimer = duration;
            AlertHUD.Instance.ShowPanelTimed(AlertHUD.PanelType.Virus, textMessage, duration);
            GameEvents.OnGameFeelProfile?.Invoke(gameFeelProfile);
            StartCoroutine(ApplyVirus());
        }

        public override void RemoveEffect(PlayerController player)
        {
            GameEvents.OnGameFeelEnd?.Invoke();
            AlertHUD.Instance.ForceHidePanels();
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
            float oldTimer = _currentTimer;
            _currentTimer -= Time.deltaTime;
            _currentTimer = Mathf.Clamp(_currentTimer, 0f, duration);
            if ((int)oldTimer > (int)_currentTimer) // Remove percent score each seconds
            {
                GameEvents.OnRemovePercentPoints?.Invoke(0.1f);
            }
        }
    }
}