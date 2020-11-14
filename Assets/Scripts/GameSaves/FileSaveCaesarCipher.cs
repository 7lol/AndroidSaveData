using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileSaveCaesarCipher : FileSave {
    /// <summary>
    /// Setup save type
    /// </summary>
    public override void Awake() {
        save = new CaesarCipherIntSave(0);
    }

    [Serializable]
    public class CaesarCipherIntSave : SimpleIntSave {
        private int GetShift { get => 13377331; }
        public CaesarCipherIntSave(int newValue) {
            value = newValue;
        }
        public override string GetJson() {
            CaesarCipherIntSave tempSave = new CaesarCipherIntSave(value + GetShift);
            return JsonUtility.ToJson(tempSave);
        }
        public override void FromJson(string json) {
            JsonUtility.FromJsonOverwrite(json, this);
            value -= GetShift;
        }
    }
}
