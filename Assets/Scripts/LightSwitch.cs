using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class LightSwitch : Interactable {

    public List<Lamp> lights;
    public bool isTurnedOn;
    public AudioClipGroup sound;

    private void Start() {
        foreach (Lamp lamp in lights) 
            changeState(lamp);
    }

    public override void interact() {
        sound.play();
        isTurnedOn = !isTurnedOn;
        if (isTurnedOn)
            transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
        else
            transform.rotation = Quaternion.Euler(new Vector3(180, 0, 180));

        foreach (Lamp lamp in lights) {
            changeState(lamp);
        }
    }

    private void changeState(Lamp lamp) {
        if (isTurnedOn)
            lamp.turnOn();
        else
            lamp.turnOff();
    }

}
