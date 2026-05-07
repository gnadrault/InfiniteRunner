using System.Collections;
using UnityEngine;

namespace Feedback
{
    public class SlowMotion : MonoBehaviour
    {
        public float slowTime = 0.2f;
        private bool isSlowed = false;

        [SerializeField] private float timeSlowed = 0f;

        public static SlowMotion instance;

        private bool once;


        private void Awake()
        {
            instance = this;
        }

        void Update()
        {
            //Touche de debug pour tester le slow motion
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartSlowMotion(0.5f, 0f);
            }

            //Ralentit le temps et augmente le frameRate pour ne pas faire perte de fps
            if (timeSlowed > 0 && isSlowed == false)
            {
                isSlowed = true;
                //TimeManager.Instance.SetTimeScale(slowTime);
                Time.fixedDeltaTime = slowTime * Time.deltaTime;

            }
            //Quand plus de temps, remet a la normal
            else if(timeSlowed <= 0 && isSlowed == true)
            {
                isSlowed = false;
                //TimeManager.Instance.SetTimeScale(1);
                //Time.fixedDeltaTime = Time.timeScale;
            }

        
            if (isSlowed)
            {
                timeSlowed -= Time.deltaTime;
            }
            else
                timeSlowed = 0f;
        }

        //Fonction a appeler pour commencer le slow motion, duration = le temps du slowmo, waitbeforeSM = le temps a attendre avant de le d�marr�
        public void StartSlowMotion(float duration, float WaitBeforeSM)
        {
            StartCoroutine(WaitBeforeSlowMotion(duration, WaitBeforeSM));
        }

        public IEnumerator WaitBeforeSlowMotion(float duration, float wait)
        {
            yield return new WaitForSeconds(wait);

            timeSlowed = duration;

        }
    }
}
