using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DUCK.Crypto;
using SimpleJSON;

public class FileSaveEncrypted  : FileSave {
    /// <summary>
    /// Setup save type
    /// </summary>
    public override void Awake() {
        save = new EncryptedIntSave(0);
    }

    [Serializable]
    public class EncryptedIntSave : SimpleIntSave {
        public string EncryptionKey { get => 13377331.ToString() + SystemInfo.deviceUniqueIdentifier; }
        public EncryptedIntSave(int newValue) {
            value = newValue;
        }
        public override string GetJson() {
            var encrypted = SimpleAESEncryption.Encrypt(JsonUtility.ToJson(this), EncryptionKey);
            JSONNode rootNode = new JSONObject();
            rootNode["json"] = encrypted.EncryptedText;
            rootNode["password"] = encrypted.IV;
            return rootNode.ToString();
        }

        public override void FromJson(string json) {
            JSONNode rootNode = JSONNode.Parse(json);
            if (rootNode["json"].IsString && rootNode["password"].IsString) {
                var decryptedJson = SimpleAESEncryption.Decrypt(rootNode["json"].Value, rootNode["password"].Value, EncryptionKey);
                JsonUtility.FromJsonOverwrite(decryptedJson, this);
            }
        }

        protected static string CreateHash(string toBeHashed) {
            byte[] toBeHashedBytes = System.Text.Encoding.UTF8.GetBytes(toBeHashed);
            toBeHashedBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(toBeHashedBytes);
            return Convert.ToBase64String(toBeHashedBytes);
        }
    }
}
