using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {

    private GameObject darkness;
    private Renderer renderer;

    public Color emissiveColor;
    public float intensity;

    private bool on = true;

    void Awake() {
        renderer = GetComponent<Renderer>();
    }

	void Start () {
       // darkness = transform.Find("Darkness").gameObject;
       // darkness.SetActive(false);
	    
	}

    public void changeState() {
        if (on) {
            turnOff();
        }
        else {
            turnOn();
        }
    }

    public void turnOn() {
       // if (l != null) {
       //     l.gameObject.SetActive(true);
      //      if (darkness != null)
        //        darkness.gameObject.SetActive(false);
     //   }
        on = true;
        DynamicGI.SetEmissive(renderer, emissiveColor * intensity);
        renderer.materials[1].SetColor("_EmissionColor", emissiveColor);
    }

    public void turnOff() {
     //   l.gameObject.SetActive(false);
    //    if (darkness != null)
      //      darkness.gameObject.SetActive(true);
        on = false;
        DynamicGI.SetEmissive(renderer, Color.black);
        renderer.materials[1].SetColor("_EmissionColor", Color.black * -5);
    }

}
