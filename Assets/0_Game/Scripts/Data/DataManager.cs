using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[DefaultExecutionOrder(-9)]
public class DataManager : Singleton<DataManager>
{
    private Data _data;
    private string _path;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        _path = Path.Combine(Application.persistentDataPath, "data.json");
        _data = new Data();
        if (LoadData() == null)
        {
            _data.HighScore = 0;
            SaveData();
        }
    }

    private Data LoadData()
    {
        if (!File.Exists(_path)) return null;
        string json = File.ReadAllText(_path);
        _data = JsonUtility.FromJson<Data>(json);
        return _data;
    }
    public void SaveData()
    {
        string json = JsonUtility.ToJson(_data);
        File.WriteAllText(_path, json);
    }

    public Data GameData { get { return _data; } }
}
