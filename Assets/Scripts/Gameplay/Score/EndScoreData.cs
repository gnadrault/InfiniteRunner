using System;

namespace Gameplay.Score
{
    /// <summary>
    /// Wrapper for the score and high score status
    /// </summary>
    [Serializable]
    public class EndScoreData
    {
        public int score;
        public bool isNewHighScore;

        public EndScoreData(int score, bool isNewHighScore)
        {
            this.score = score;
            this.isNewHighScore = isNewHighScore;
        }
    }
}