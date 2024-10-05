using System.IO;
using UnityEngine;

public static class SettingsManager 
{
    private static string settingsFilePath = Application.persistentDataPath + "/gamesettings.json";

    public static void SaveSettings (GameSettings settings)
    {
        string json = JsonUtility.ToJson(settings, true);
        File.WriteAllText(settingsFilePath, json);
    }

    public static GameSettings LoadSettings()
    {
        if (File.Exists(settingsFilePath))
        {
            string json = File.ReadAllText(settingsFilePath);
            return JsonUtility.FromJson<GameSettings>(json);
        }
        else
        {
            return new GameSettings(); // Return default settings if no file exists
        }
    }
}
