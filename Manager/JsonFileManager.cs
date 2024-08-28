using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonFileManager<T>
{
    static JsonFileManager<T> instance;

    public static JsonFileManager<T> Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new JsonFileManager<T>();
            }

            return instance;
        }
    }

    public void Write(T data, string fileName)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.dataPath, fileName + ".json");
        File.WriteAllText(path, jsonData);
    }

    public T Read(string fileName)
    {
        string path = Path.Combine(Application.dataPath, fileName + ".json");
        if (File.Exists(path) == false)
        {
            return default;
        }

        string jsonData = File.ReadAllText(path);

        return JsonUtility.FromJson<T>(jsonData);
    }
}
