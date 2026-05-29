using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class ScoreSaveData
    {
        public int score;
        public string date;
        
        public ScoreSaveData(int value)
        {
            score = value;
            date = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }
    }
    
    [Serializable]
    public class ScoreData
    {
        public List<ScoreSaveData> entries = new();
    }
}