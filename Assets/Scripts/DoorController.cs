using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class DoorController : Interactable {

    public float duration = 2;
    [Range(-1, 1)]
    public int direction = 1;

    private bool closed = true;
    private float elapsed;



    public override void interact() {
        StopCoroutine("openDoor");
        StopCoroutine("closeDoor");
        if (!closed) {
            StartCoroutine("closeDoor");
        } else {
            StartCoroutine("openDoor");
        }
        closed = !closed;
    }

    private IEnumerator openDoor() {
        Quaternion from = Quaternion.Euler(180f, 0f, 0f);
        Quaternion to = Quaternion.Euler(180f, 0f, 110f * direction);

        while(elapsed < duration){
            transform.localRotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        elapsed = duration;
    }

    private IEnumerator closeDoor() {
        Quaternion from = Quaternion.Euler(180f, 0f, 0f);
        Quaternion to = Quaternion.Euler(180f, 0f, 110f * direction);
        
        while (elapsed > 0) {
            transform.localRotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed -= Time.deltaTime;
            yield return null;
        }
        elapsed = 0f;
    }
}
