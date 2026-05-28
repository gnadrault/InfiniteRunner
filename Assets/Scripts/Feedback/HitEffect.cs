using System.Collections;
using Feedback.Data;
using TMPro;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Feedback
{
    public class HitEffect : MonoBehaviour, IGameFeelEffect
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private float hitDuration = 0.5f;

        private Vector3 _initialPos;
        private Color _initialColor;
        private Coroutine _hitCoroutine;

        private ScoreSection _scoreProfil;
        
        private void OnEnable() => GameEvents.OnGameFeelProfile += ApplyEffect;
        private void OnDisable() => GameEvents.OnGameFeelProfile -= ApplyEffect;

        private void Awake()
        {
            _initialPos = label.transform.localPosition;
            _initialColor = label.color;
        }

        private void ApplyEffect(GameFeelProfile profile)
        {
            if (!profile || !profile.Score.enabled) return;
            _scoreProfil = profile.Score;
            if (_hitCoroutine != null)
            {
                StopCoroutine(_hitCoroutine);
                label.transform.localPosition = _initialPos;
            }
            _hitCoroutine = StartCoroutine(HitTrigger());
        }

        public void ResetToAmbient()
        {
            throw new System.NotImplementedException();
        }

        private IEnumerator HitTrigger()
        {
            float elapsed = 0f;

            while (elapsed < hitDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / hitDuration;

                label.transform.localPosition = _initialPos + Random.insideUnitSphere * _scoreProfil.shakeIntensity;
                label.color = Color.Lerp(_scoreProfil.color, _initialColor, t);
                yield return null;
            }

            label.transform.localPosition = _initialPos;
            label.color = _initialColor;
            _hitCoroutine = null;
        }

        void IGameFeelEffect.ApplyEffect(GameFeelProfile profile)
        {
            ApplyEffect(profile);
        }
    }
}