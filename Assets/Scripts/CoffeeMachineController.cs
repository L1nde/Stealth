using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class CoffeeMachineController : Interactable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void interact() {
        var coffee = GetComponentInChildren<Coffee>();
        coffee.gameObject.SetActive(false);
        if (GetComponentInChildren<Coffee>() == null) {
            disable();
            return;
        }
    }
}
