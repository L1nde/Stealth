using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class LightSwitch : Interactable {

    public List<Lamp> lights;
    public bool isTurnedOn = true;
    public AudioClipGroup sound;

    private Animator anim;

    public AIFollow enemyThatLooksAfter;

    private void Start() {
        anim = GetComponent<Animator>();
        // foreach (Lamp lamp in lights) 
        // changeState(lamp);
    }

    public override void interact() {
        sound.playAtLocation(transform.position);
        isTurnedOn = !isTurnedOn;
        //child.transform.Rotate(new Vector3(0, 0, 1), 10);
        anim.SetTrigger("Interact");

        foreach (Lamp lamp in lights) {
            lamp.changeState();
        }
    }


    //private void changeState(Lamp lamp) {
      //  if (isTurnedOn)
      //      lamp.turnOn();
      //  else {
      //      lamp.turnOff();
       //     if (enemyThatLooksAfter != null)
       //         enemyThatLooksAfter.turnOnTheSwitch(this);
     //   }
 //   }

}
