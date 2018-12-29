using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {

    private GameObject darkness;
    private Light l;
    public bool isTurnedOn;

	void Start () {
        darkness = transform.Find("Darkness").gameObject;
        l = GetComponentInChildren<Light>();
        darkness.SetActive(false);
	}

    public void changeState() {
        isTurnedOn = !isTurnedOn;
        if (isTurnedOn)
            turnOn();
        else
            turnOff();
    }

    private void turnOn() {
        l.gameObject.SetActive(true);
        darkness.gameObject.SetActive(false);
    }

    private void turnOff() {
        l.gameObject.SetActive(false);
        darkness.gameObject.SetActive(true);
    }

}
