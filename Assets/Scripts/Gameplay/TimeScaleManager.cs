using UnityEngine;

namespace Gameplay
{
    public class TimeScaleManager : MonoBehaviour

    {
        public static TimeScaleManager Instance;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }


        private bool AllowTimeModification()
        {
            return true;
        }


        public void SetTimeScale(float scale)
        {
            if (!AllowTimeModification())
                return;

            Time.timeScale = scale;
        }

        /*private void OnEnable()
        {
            GameEvents.OnGameStateChanged += HandleState;
        }

        private void OnDisable()
        {
            GameEvents.OnGameStateChanged -= HandleState;
        }

        /// <summary>
        /// Handle the timeScale according to the new game state received
        /// </summary>
        /// <param name="state"></param>
        private void HandleState(GameState state)
        {
            switch (state)
            {
                case GameState.Gameplay:
                    Time.timeScale = 1f;
                    break;
                case GameState.Pause:
                case GameState.End:
                    Time.timeScale = 0f;
                    break;
            }
        }*/
    }
}