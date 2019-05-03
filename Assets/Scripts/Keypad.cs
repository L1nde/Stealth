using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour {

    public Text screen;
    public WinTrigger WinTrigger;

    private string code;

    public void Click(int num) {
        if (screen.text.Length > 6) {
            return;
        }

        code += num;
        UpdateScreen(code);
        
    }

    public void Clear() {
        code = "";
        UpdateScreen("");
    }

    public void Enter() {
        WinTrigger.ShouldWin(code);
        Clear();
    }

    private void UpdateScreen(string text) {
        screen.text = text;
    }
}
