using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static string GetSavePath(string saveName)
    {
        return Path.Combine(Application.persistentDataPath, saveName + ".json");
    }

    public static void SaveToSlot(SaveData data, string saveName)
    {
        string path = GetSavePath(saveName);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Сохранено в " + path);
    }

    public static SaveData LoadFromSlot(string saveName)
    {
        string path = GetSavePath(saveName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }

        Debug.LogWarning("Сейв не найден: " + path);
        return null;
    }

    public static void DeleteSlot(string saveName)
    {
        string path = GetSavePath(saveName);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static string[] GetAllSaveSlots()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.json");
        for (int i = 0; i < files.Length; i++)
            files[i] = Path.GetFileNameWithoutExtension(files[i]);
        return files;
    }
}