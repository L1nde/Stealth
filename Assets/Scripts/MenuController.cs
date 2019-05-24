using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    public Text PlayText;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Play() {
        PlayText.text = "Loading";
        SceneManager.LoadScene("FreshStart");
    }

    public void Exit() {
        Application.Quit();
    }
}
