using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class FileSaveWithHash : FileSave {
    /// <summary>
    /// Setup save type
    /// </summary>
    public override void Awake() {
        save = new HashIntSave(0);
    }

    [Serializable]
    public class HashIntSave : SimpleIntSave {
        public HashIntSave(int newValue) {
            value = newValue;
        }
        public override string GetJson() {
            JSONNode rootNode = new JSONObject();
            rootNode["json"] = JSONNode.Parse(JsonUtility.ToJson(this));
            rootNode["hash"] = CreateHash(rootNode["json"]);
            return rootNode.ToString();
        }

        public override void FromJson(string json) {
            JSONNode rootNode = JSONNode.Parse(json);

            if (rootNode["json"].IsString && rootNode["hash"] == CreateHash(rootNode["json"])) {
                JsonUtility.FromJsonOverwrite(rootNode["json"], this);
            }
        }

        protected static string CreateHash(string toBeHashed) {
            byte[] toBeHashedBytes = System.Text.Encoding.UTF8.GetBytes(toBeHashed);
            toBeHashedBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(toBeHashedBytes);
            return Convert.ToBase64String(toBeHashedBytes);
        }
    }
}
