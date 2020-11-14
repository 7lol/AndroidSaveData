using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileSave : GameSave {
    protected SimpleIntSave save;
    /// <summary>
    /// Setup save type
    /// </summary>
    public virtual void Awake(){
        save = new SimpleIntSave(0);
    }

    public override void Save() {
        string path = $"{Application.persistentDataPath}/{saveName}.json";
        save.value = value;
        string saveJson = save.GetJson();
        System.IO.File.WriteAllText(path, saveJson);
        Debug.LogError($"Saved: {path}");
    }
    public override void Load() {
        string path = $"{Application.persistentDataPath}/{saveName}.json";
        if (System.IO.File.Exists(path)) {
            string jsonString = System.IO.File.ReadAllText(path);
            save.FromJson(jsonString);
            value = save.value;
        }
    }

    [Serializable]
    public class SimpleIntSave {
        [SerializeField]
        public int value;
        public SimpleIntSave() {
            value = 0;
        }
        public SimpleIntSave(int newValue)  {
            value = newValue;
        }
        public virtual string GetJson() => JsonUtility.ToJson(this);
        public virtual void FromJson(string json) => JsonUtility.FromJsonOverwrite(json, this);
    }

    /// <summary>
    /// Example template class for other data types
    /// </summary>
    [Serializable]
    public class SimpleSave<T> {
        [SerializeField]
        public T value;
        public SimpleSave(T newValue){
            value = newValue;
        }
        public virtual string GetJson() => JsonUtility.ToJson(this);
        public virtual void FromJson(string json) => JsonUtility.FromJsonOverwrite(json, this);
    }
}
