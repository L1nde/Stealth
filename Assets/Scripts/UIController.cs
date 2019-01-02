using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController instance;

    public GameObject crossHair;
    public Text interactableText;
    public GameObject winScreen;
    public GameObject loseScreen;

	// Use this for initialization
	void Awake () {
	    if (instance == null)
	        instance = this;

	    else if (instance != this)
	        Destroy(gameObject);
    }

    public void enableInteractable() {
        interactableText.gameObject.SetActive(true);
    }

    public void disableInteractable() {
        interactableText.gameObject.SetActive(false);
    }

    public void showWinScreen() {
        crossHair.SetActive(false);
        winScreen.SetActive(true);
    }

    public void showLoseScreen() {
        crossHair.SetActive(false);
        loseScreen.SetActive(true);
    }


}
