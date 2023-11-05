using System;
using System.IO;
using UnityEngine;

public static class GameSave
{
    public const string GameSaveKey = "HasGameSave";
    public const string SaveName = "slot0.gamesave";

    public static bool HasGameSaveFile()
    {
        if (!PlayerPrefs.HasKey(GameSaveKey))
            return false;

        if (!File.Exists(Path.Combine(Application.persistentDataPath, SaveName)))
        {
            PlayerPrefs.DeleteKey(GameSaveKey);
            return false;
        }

        return true;
    }

    public static SaveFile LoadGameSave()
    {
        var text = File.ReadAllText(Path.Combine(Application.persistentDataPath, SaveName));

        var file = JsonUtility.FromJson<SaveFile>(text);

        Debug.Log("Loaded save file");
        //TODO: check jsonResult
        return file;
    }

    public static void SaveGame()
    {
        PlayerPrefs.SetInt(GameSaveKey, 1);

        SaveFile game = GameState.CurrentState;
        string text = JsonUtility.ToJson(game);

        File.WriteAllText(Path.Combine(Application.persistentDataPath, SaveName), text);
    }
}


[Serializable]
public class SaveFile
{
    public Star[] stars;

    public SaveFile(Star[] stars)
    {
        this.stars = stars;
    }
}
