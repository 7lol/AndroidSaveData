using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameSave : MonoBehaviour {
    public int value = 0;
    public int tmpValue = 0;
    public string saveName = "coins";
    public TextMeshProUGUI label;
    public UnityEvent<int> onTempValueChanged = new UnityEvent<int>();
    /// <summary>
    /// Handle loading and filling data on start.
    /// </summary>
    public void Start() {
        Load();
        RefreshLabel();
        onTempValueChanged.Invoke(value);
    }
    /// <summary>
    /// Set value, and save it.
    /// </summary>
    public void SetValue() {
        value = tmpValue;
        RefreshLabel();
        Save();
    }
    /// <summary>
    /// Refresh Value label
    /// </summary>
    public void RefreshLabel() {
        label.text = value.ToString();

    }
    /// <summary>
    /// Set tmp Value, and update tmpValue label.
    /// </summary>
    public void SoftSetValue(string value) {
        if (int.TryParse(value, out int result)) {
            tmpValue = result;
            onTempValueChanged.Invoke(result);
        }
    }

    /// <summary>
    /// Save value in persistent memory
    /// </summary>
    public virtual void Save() { }
    /// <summary>
    /// Load value from persistent memory
    /// </summary>
    public virtual void Load() { }
}
