using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController instance;

    public GameObject crossHair;
    public GameObject interactableText;
    public GameObject interactableText2;
    public GameObject interactablePic;
    public GameObject winScreen;
    public GameObject loseScreen;
    public Image CoffeeBuff;

	// Use this for initialization
	void Awake () {
	    if (instance == null)
	        instance = this;

	    else if (instance != this)
	        Destroy(gameObject);
        CoffeeBuff.gameObject.SetActive(false);
	    interactableText2.gameObject.SetActive(false);
	    interactableText.gameObject.SetActive(false);
	    interactablePic.gameObject.SetActive(false);
    }

    public void enableInteractable() {
        interactableText.gameObject.SetActive(true);
    }

    public void disableInteractable() {
        interactableText.gameObject.SetActive(false);
    }

    public void enableInteractable2() {
        interactableText2.gameObject.SetActive(true);
    }

    public void disableInteractable2() {
        interactableText2.gameObject.SetActive(false);
    }

    public void enableInteractablePic() {
        interactablePic.gameObject.SetActive(true);
    }

    public void disableInteractablePic() {
        interactablePic.gameObject.SetActive(false);
    }

    public void coffeeBuff(float timeLeft) {
        if (timeLeft <= 0) {
            CoffeeBuff.gameObject.SetActive(false);
        }
        else {
            CoffeeBuff.gameObject.SetActive(true);
        }
        Debug.Log(timeLeft);
        CoffeeBuff.fillAmount = timeLeft;
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
