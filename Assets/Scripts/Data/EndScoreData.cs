using System;

namespace Data
{
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