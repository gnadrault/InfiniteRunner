using System;
using Core;
using Data;
using Feedback.Data;
using Gameplay.Segments;

namespace Utils
{
    /// <summary>
    /// Utility class to manage global events
    /// </summary>
    public abstract class GameEvents
    {
        #region Player
        public static Action OnPlayerDied;
        public static Action OnVirusAttached;
        #endregion

        #region Gameplay
        public static Action<float> OnNewMeter;
        public static Action<float> OnSpeedChanged;
        public static Action<Segment> OnSegmentDestroyed;
        public static Action<PhaseData> OnNewPhase;
        public static Action<GameState> OnGameStateChanged;
        public static Action<EndScoreData> OnEndMenu;
        public static Action OnEndGame;
        #endregion
        
        #region Effects
        public static Action<GameFeelProfile> OnGameFeelProfileStart;
        public static Action OnGameFeelEnd;
        public static Action<GameFeelProfile> OnStroboscopeEffectStart;
        public static Action OnStroboscopeEffectEnd;
        #endregion

        #region Score
        public static Action<float> OnRemovePercentPoints;
        #endregion

        #region Letters
        public static Action<string, bool> OnLetterCollected;
        public static Action<int> OnWordCompleted;
        #endregion

        #region Malus/Bonus
        public static Action OnShieldBroken;
        public static Action OnGhostBroken;
        #endregion
    }
}