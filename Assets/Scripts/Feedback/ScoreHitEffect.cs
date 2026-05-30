using System.Collections;
using Feedback.Data;
using TMPro;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Feedback
{
    public class ScoreHitEffect : MonoBehaviour, IGameFeelEffect
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private float hitDuration = 0.4f;

        private Vector3 _initialPos;
        private Color _initialColor;
        private Coroutine _routine;

        private void OnEnable()
        {
            GameEvents.OnGameFeelProfileStart += ApplyEffect;
            GameEvents.OnGameFeelEnd += ResetToAmbient;
        }

        private void OnDisable()
        {
            GameEvents.OnGameFeelProfileStart -= ApplyEffect;
            GameEvents.OnGameFeelEnd -= ResetToAmbient;
        }

        private void Awake()
        {
            _initialPos = label.transform.localPosition;
            _initialColor = label.color;
        }

        public void ApplyEffect(GameFeelProfile profile)
        {
            if (!profile || !profile.Score.enabled) return;

            StopCurrent();
            _routine = StartCoroutine(PulseRoutine(profile.Score));
        }

        public void ResetToAmbient()
        {
            StopCurrent();
            label.transform.localPosition = _initialPos;
            label.color = _initialColor;
        }

        private IEnumerator PulseRoutine(ScoreSection scoreProfil)
        {
            while (true)
            {
                yield return SingleHit(scoreProfil);
                yield return new WaitForSeconds(1f - hitDuration);
            }
        }

        private IEnumerator SingleHit(ScoreSection scoreProfil)
        {
            float elapsed = 0f;

            _initialPos = label.transform.localPosition;
            _initialColor = label.color;

            while (elapsed < hitDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / hitDuration;

                label.transform.localPosition = _initialPos + Random.insideUnitSphere * scoreProfil.shakeIntensity;
                label.color = Color.Lerp(scoreProfil.color, _initialColor, t);
                yield return null;
            }

            label.transform.localPosition = _initialPos;
            label.color = _initialColor;
        }

        private void StopCurrent()
        {
            if (_routine == null) return;
            StopCoroutine(_routine);
            _routine = null;
        }
    }
}