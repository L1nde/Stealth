using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class LightSwitch : Interactable {

    public List<Lamp> lights;
    public bool isTurnedOn;
    public AudioClipGroup sound;

    public AIFollow enemyThatLooksAfter;

    private void Start() {
        foreach (Lamp lamp in lights) 
            changeState(lamp);
    }

    public override void interact() {
        sound.playAtLocation(transform.position);
        isTurnedOn = !isTurnedOn;
        transform.Rotate(new Vector3(0, 0, 1), 180);

        foreach (Lamp lamp in lights) {
            changeState(lamp);
        }
    }


    private void changeState(Lamp lamp) {
        if (isTurnedOn)
            lamp.turnOn();
        else {
            lamp.turnOff();
            if (enemyThatLooksAfter != null)
                enemyThatLooksAfter.turnOnTheSwitch(this);
        }
    }

}
