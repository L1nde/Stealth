using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Consumable {
    private float currentDuration;
    public float duration;
    public Mesh emptyCup;

    public override void consume() {
        player.GetComponentInChildren<AcidTrip>().activate(duration);
        gameObject.GetComponent<MeshFilter>().mesh = emptyCup;
        Destroy(this);
    }



}
