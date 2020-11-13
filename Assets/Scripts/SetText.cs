using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetText : MonoBehaviour {
    public string text;
    public TextMeshProUGUI label;
    public void FormatTextWithValue(string value) { 
        if (int.TryParse(value, out int result)) {
            FormatTextWithValue(result);
        }
    }
    public void FormatTextWithValue(int value) {
        label.text = string.Format(text, value);
    }
}
