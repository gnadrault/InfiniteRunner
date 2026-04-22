using System.IO;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    public StageSave stageSave;
    
    public void SaveToJSON()
    {
        string json = JsonUtility.ToJson(stageSave);
        string path = Path.Combine(Application.persistentDataPath + "stage.json");
        File.WriteAllText(path, json);
    }

    public void LoadFromJSON()
    {
        string path = Path.Combine(Application.persistentDataPath + "stage.json");
        string json = File.ReadAllText(path);
        stageSave = JsonUtility.FromJson<StageSave>(json);
    }
}

[System.Serializable]
public class StageSave
{
    public int stage;
    public string characterName;
    public int playerLevel;
    public int amountGold;
    public Race playerRace = Race.Dwarf;
}

public enum Race { Elf, Dwarf, Hobbit, Gnome, Human, Gobelin }
