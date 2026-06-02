using System.IO;
using System.Linq;
using Data;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Save and Load scores
    /// </summary>
    public class ScoreSave : GameBehavior
    {
        
        private static string FilePath => Path.Combine(Application.persistentDataPath, "scores.json");

        /// <summary>
        /// Add a new score to the current saved file
        /// Order by descending
        /// Write to the file
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool AddScore(int score)
        {
            ScoreData data = Load();
            
            bool isNewBest = data.entries.Count == 0 || score > data.entries[0].score;
            
            data.entries.Add(new ScoreSaveData(score));
            data.entries = data.entries
                .OrderByDescending(e => e.score) // Sort by highest score
                .Take(10) // Top 10
            .ToList();
            
            File.WriteAllText(FilePath, JsonUtility.ToJson(data, prettyPrint: true));
            return isNewBest;
        }

        /// <summary>
        /// Load saved scores from file
        /// </summary>
        /// <returns></returns>
        public static ScoreData Load()
        {
            if (!File.Exists(FilePath))
                return new ScoreData();
            
            string json = File.ReadAllText(FilePath);
            return JsonUtility.FromJson<ScoreData>(json) ?? new ScoreData();
        }
        
    }
}