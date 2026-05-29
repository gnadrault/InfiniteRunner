using System.IO;
using System.Linq;
using Data;
using UnityEngine;

namespace Core
{
    public class ScoreSave : MonoBehaviour
    {
        
        private static string FilePath => Path.Combine(Application.persistentDataPath, "scores.json");

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

        public static ScoreData Load()
        {
            if (!File.Exists(FilePath))
                return new ScoreData();
            
            string json = File.ReadAllText(FilePath);
            return JsonUtility.FromJson<ScoreData>(json) ?? new ScoreData();
        }
        
    }
}