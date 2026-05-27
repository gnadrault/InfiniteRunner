using System;
using Data;

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
        public static Action<PhaseData> OnNewPhase;
        #endregion

        #region Score
        public static Action<float> OnRemovePercentPoints;
        #endregion

        #region Letters
        public static Action<string> OnLetterCollected;
        public static Action<int> OnWordCompleted;
        #endregion

        #region Effects
        public static Action OnShieldBroken;
        public static Action OnGhostBroken;
        #endregion
    }
}