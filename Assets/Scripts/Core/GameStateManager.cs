using System;
using Utils;

namespace Core
{
    public class GameStateManager : GameBehavior
    {
        public static GameStateManager Instance;
        public GameState State { get; private set; } = GameState.Gameplay;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            TimeManager.Instance.SetPaused(false);
        }

        public void SetState(GameState next)
        {
            if (State == next) return;
            State = next;
            GameEvents.OnGameStateChanged?.Invoke(next);
            TimeManager.Instance.SetPaused(next != GameState.Gameplay);
        }
    }
}