using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;
    private static string saveDirectoryPath;

    public List<ScriptableObject> dataToLoad; // List of ScriptableObjects to be loaded at startup

    private void Awake()
    {
        // Ensure only one instance of SaveManager exists
        if (instance == null)
        {
            instance = this;
            saveDirectoryPath = Application.persistentDataPath + "/SaveData/";
            if (!Directory.Exists(saveDirectoryPath))
            {
                Directory.CreateDirectory(saveDirectoryPath);
            }
            DontDestroyOnLoad(gameObject); // Keep SaveManager alive between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Load data when the application starts
        foreach (var data in dataToLoad)
        {
            LoadData(data);
        }
    }

    public static void SaveData(ScriptableObject data)
    {
        string jsonData = JsonUtility.ToJson(data);
        string saveFilePath = saveDirectoryPath + data.name + ".json";
        File.WriteAllText(saveFilePath, jsonData);
    }

    public static void LoadData(ScriptableObject data)
    {
        string saveFilePath = saveDirectoryPath + data.name + ".json";
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);
            JsonUtility.FromJsonOverwrite(jsonData, data);
        }
    }
}
