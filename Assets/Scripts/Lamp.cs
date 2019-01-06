using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {

    private GameObject darkness;
    private Light l;
    private Renderer renderer;

    public Color emissiveColor;
    public float intensity;

    void Awake() {
        renderer = GetComponent<Renderer>();
    }

	void Start () {
        darkness = transform.Find("Darkness").gameObject;
        l = GetComponentInChildren<Light>();
        darkness.SetActive(false);
	    
	}

    

    public void turnOn() {
        if (l != null) {
            l.gameObject.SetActive(true);
            if (darkness != null)
                darkness.gameObject.SetActive(false);
        }
        DynamicGI.SetEmissive(renderer, emissiveColor);
        renderer.materials[1].SetColor("_EmissionColor", emissiveColor * intensity);
    }

    public void turnOff() {
        l.gameObject.SetActive(false);
        if (darkness != null)
            darkness.gameObject.SetActive(true);
        DynamicGI.SetEmissive(renderer, Color.black);
        renderer.materials[1].SetColor("_EmissionColor", Color.black);
    }

}
