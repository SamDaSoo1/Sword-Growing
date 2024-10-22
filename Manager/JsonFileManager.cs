using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
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
        //string path = Path.Combine(Application.dataPath, fileName + ".json");
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
        File.WriteAllText(path, jsonData);
        //Debug.Log($"--------------------------제이슨 파일 저장중: {path}-------------------------");
    }

    public T Read(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
        //string path = Path.Combine(Application.dataPath, fileName + ".json");
        if (File.Exists(path) == false)
        {
            //Debug.Log($"--------------------------제이슨 파일 읽기 실패: {path}-------------------------");
            return default;
        }

        string jsonData = File.ReadAllText(path);
        
        return JsonUtility.FromJson<T>(jsonData);
    }
}
