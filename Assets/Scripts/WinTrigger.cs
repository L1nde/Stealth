using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour {

    public string winCode = "0000";

    public bool ShouldWin(string code) {
        if (winCode == code) {
            GameController.instance.win();
            return true;
        }

        return false;
    }
}
