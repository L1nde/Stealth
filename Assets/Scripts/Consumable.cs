using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour {
    protected GameObject player;
    public bool Used;
    public KeyCode Key = KeyCode.F;
    public string Description;

    void Start () {
	    player = GameObject.FindGameObjectWithTag("Player");
	}

    public abstract void consume();
}
