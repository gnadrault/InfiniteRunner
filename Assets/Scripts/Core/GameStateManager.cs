using Utils;

namespace Core
{
    /// <summary>
    /// Manage the global game state
    /// </summary>
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

        public void SetState(GameState nextState)
        {
            if (State == nextState) return;
            
            State = nextState;
            GameEvents.OnGameStateChanged?.Invoke(nextState);
            TimeManager.Instance.SetPaused(nextState != GameState.Gameplay);
        }
    }
}