using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Consumable {
    private float currentDuration;
    public float duration;
    public Mesh emptyCup;

    public override void consume() {
        Used = true;
        StartCoroutine(use());
        
    }

    IEnumerator use() {
        yield return new WaitForSeconds(1.5f);
        player.GetComponentInChildren<AcidTrip>().activate(duration);
        gameObject.GetComponent<MeshFilter>().mesh = emptyCup;
        Destroy(this);
    }

}
