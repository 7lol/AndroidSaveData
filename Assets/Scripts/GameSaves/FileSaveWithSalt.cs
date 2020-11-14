using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileSaveWithSalt : FileSave {
    /// <summary>
    /// Setup save type
    /// </summary>
    public override void Awake() {
        save = new SaltedIntSave(0);
    }

    [Serializable]
    public class SaltedIntSave : SimpleIntSave {
        private int GetSalt { get => 13377331; }
        public SaltedIntSave(int newValue) {
            value = newValue;
        }
        public override string GetJson() {
            SaltedIntSave tempSave = new SaltedIntSave(value + GetSalt);
            return JsonUtility.ToJson(tempSave);
        }
        public override void FromJson(string json) {
            JsonUtility.FromJsonOverwrite(json, this);
            value -= GetSalt;
        }
    }
}
