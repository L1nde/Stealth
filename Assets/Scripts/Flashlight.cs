using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Consumable {

    private Hold hold;
    private Light light;
    private bool on;

    // Start is called before the first frame update
    void Start() {
        hold = GetComponentInChildren<Hold>();
        light = GetComponentInChildren<Light>();
        TurnOff();
    }

    // Update is called once per frame
    void Update() {
        if (on && transform.parent != null) {
            //light.transform.position = Camera.main.transform.position;
            light.transform.forward = Camera.main.transform.forward;
        }
    }

    public void TurnOn() {
        on = true;
        light.enabled = true;
    }

    public void TurnOff() {
        on = false;
        light.enabled = false;
    }

    public override void consume() {
        if (on) {
            TurnOff();
        }
        else {
            TurnOn();
        }
    }
}
