using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static FileSave;

public class BinFileSave : GameSave {
    protected SimpleIntSave save;
    /// <summary>
    /// Setup save type
    /// </summary>
    public virtual void Awake(){
        save = new SimpleIntSave(0);
    }

    public override void Save() {
        string path = $"{Application.persistentDataPath}/{saveName}.bin";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        save.value = value;
        bf.Serialize(stream, save.GetJson());
        stream.Close();
        Debug.LogError($"Saved: {path}");
    }
    public override void Load() {
        string path = $"{Application.persistentDataPath}/{saveName}.bin";
        if (System.IO.File.Exists(path)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            try {
                save.FromJson(bf.Deserialize(stream) as string);
            } catch (System.Runtime.Serialization.SerializationException) {
                stream.Close();
                return;
            }
            stream.Close();
            value = save.value;
        }
    }
}
