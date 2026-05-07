using System.Collections;
using UnityEngine;

namespace Feedback
{
    public class FreezeFrame : MonoBehaviour
    {
        public static FreezeFrame instance;

        private bool isFreezing = false;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

        }

        public void Freeze(float duration)
        {
            if (!isFreezing)
                StartCoroutine(DoFreeze(duration));
        }

        private IEnumerator DoFreeze(float duration)
        {
            isFreezing = true;
            float originalTimeScale = Time.timeScale;
            
            //TimeManager.Instance.SetTimeScale(0f);
            yield return new WaitForSecondsRealtime(duration); // Realtime car le temps est figé
            //TimeManager.Instance.SetTimeScale(originalTimeScale);

            isFreezing = false;
        }
    }
}
