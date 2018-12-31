using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {

    private GameObject darkness;
    private Light l;
    

	void Start () {
        darkness = transform.Find("Darkness").gameObject;
        l = GetComponentInChildren<Light>();
        darkness.SetActive(false);
	}

    

    public void turnOn() {
        if (l != null) {
            l.gameObject.SetActive(true);
            darkness.gameObject.SetActive(false);
        }
    }

    public void turnOff() {
        l.gameObject.SetActive(false);
        darkness.gameObject.SetActive(true);
    }

}
