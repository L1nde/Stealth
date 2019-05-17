using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ibuprofenum : Consumable {
    public Mesh empty;

    public override void consume() {
        Used = true;
        StartCoroutine(Use());
    }

    private IEnumerator Use() {
        yield return new WaitForSeconds(1.5f);
        var acid = player.GetComponentInChildren<AcidTrip>();
        if (acid != null) {
           acid.deactivate(); 
        }
        GetComponent<MeshFilter>().mesh = empty;
        Destroy(this);
    }
}
