using System;

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

        #region Score

        public static Action<float> OnAddScorePoints;
        public static Action<float> OnRemovePercentPoints;

        #endregion

        #region Letters

        public static Action<string> OnLetterCollected;

        #endregion

        #region Effects

        public static Action OnShieldBroken;
        public static Action OnGhostBroken;

        #endregion
    }
}