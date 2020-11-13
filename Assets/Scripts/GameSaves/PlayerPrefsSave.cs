using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSave : GameSave {

    public override void Save() {
        PlayerPrefs.SetInt(saveName, value);
        PlayerPrefs.Save();
    }

    public override void Load() { 
        if (PlayerPrefs.HasKey(saveName)) {
            value = PlayerPrefs.GetInt(saveName);
        }
    }
}
