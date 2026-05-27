using System.Collections;
using TMPro;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Feedback
{
    public class HitEffect : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private float shakeIntensity = 3f;
        [SerializeField] private float hitDuration = 0.5f;
        [SerializeField] private Color hitColor = Color.red;

        private Vector3 _initialPos;
        private Color _initialColor;
        private Coroutine _hitCoroutine;

        private void Awake()
        {
            _initialPos = label.transform.localPosition;
            _initialColor = label.color;
        }

        private void OnEnable()
        {
            GameEvents.OnRemovePercentPoints += OnHit;
        }

        private void OnDisable()
        {
            GameEvents.OnRemovePercentPoints -= OnHit;
        }

        private void OnHit(float obj)
        {
            if (_hitCoroutine != null)
            {
                StopCoroutine(_hitCoroutine);
                label.transform.localPosition = _initialPos;
            }

            _hitCoroutine = StartCoroutine(HitTrigger());
        }

        private IEnumerator HitTrigger()
        {
            float elapsed = 0f;

            while (elapsed < hitDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / hitDuration;

                label.transform.localPosition = _initialPos + Random.insideUnitSphere * shakeIntensity;
                label.color = Color.Lerp(hitColor, _initialColor, t);
                yield return null;
            }

            label.transform.localPosition = _initialPos;
            label.color = _initialColor;
            _hitCoroutine = null;
        }
    }
}