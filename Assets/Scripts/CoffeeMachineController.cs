using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class CoffeeMachineController : Interactable { //todo class is not being used. delete
    private GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void interact() {
        var coffee = GetComponentInChildren<Holdable>();
        player.GetComponent<ItemHolding>().pickUp(coffee);
        coffee.transform.parent = null;
        if (GetComponentInChildren<Coffee>() == null) {
            disable();
            return;
        }
    }
}
