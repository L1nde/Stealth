/*
 * Author : Maxime JUMELLE
 * Namespace : AcidTrip
 * Project : AcidTrip
 * 
 * If you have any suggestion or comment, you can write me at webmaster[at]hardgames3d.com
 * 
 * File : AcidTrip.cs
 * Abstract : This is the main script that allows you to creates an acid trip effect on camera.
 * 
 * */

using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AcidTrip : MonoBehaviour {
    private float timer = 0;

    public float Wavelength = 1.0f, DistortionStrength = 0.25f;
    public bool Sparkling = false;

    public float SaturationBase = 1.0f, SaturationSpeed = 1.0f, SaturationAmplitude = 0.3f;

    public Shader currentShader = null;
    private Material currentMaterial = null;
    public Blackout Blackout;

    private float currentDuration;

    void Start() {

        Blackout = GetComponent<Blackout>();
    }

    public bool CheckResources() {
        currentShader = Shader.Find("AcidTrip/AcidTrip");
        currentMaterial = new Material(currentShader);
        return true;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        if (!CheckResources()) {
            Graphics.Blit(source, destination);
            return;
        }

        timer += Time.deltaTime;
        currentMaterial.SetFloat("timer", timer);
        currentMaterial.SetFloat("speed", 1);
        currentMaterial.SetFloat("distortion", 0.25f);
        currentMaterial.SetFloat("amplitude", 70.0f);
        currentMaterial.SetFloat("satbase", SaturationBase);
        currentMaterial.SetFloat("satSpeed", SaturationSpeed);
        currentMaterial.SetFloat("satAmp", SaturationAmplitude);
        currentMaterial.SetFloat("strength", Wavelength);
        currentMaterial.SetFloat("distortion", DistortionStrength);
        currentMaterial.SetInt("sparkling", (Sparkling) ? 1 : 0);


        Graphics.Blit(source, destination, currentMaterial);
    }

    public void activate(float duration) {
        enabled = true;
        currentDuration += duration;
        StopCoroutine("activateCoroutine");
        StartCoroutine("activateCoroutine");

    }

    public void deactivate() {
        enabled = false;
        currentDuration = 0f;
        StopAllCoroutines();
        if (Blackout != null) {
            Blackout.Timer = 0;
            Blackout.StopEffect();
        }
        UIController.instance.coffeeBuff(0);
    }

    public bool IsActive() {
        return enabled;
    }

    private IEnumerator activateCoroutine() {
        var max = currentDuration;
        yield return new WaitForEndOfFrame();
        Blackout.Timer = 0;
        yield return new WaitForEndOfFrame();
        Blackout.StopEffect();
        GameController.instance.Invulnarable = true;
        while (currentDuration > 0) {
            currentDuration -= Time.deltaTime;
            UIController.instance.coffeeBuff(currentDuration / max);
            yield return null;
        }

        GameController.instance.Invulnarable = false;
        Blackout.StartEffect();
        currentDuration = 0f;
        enabled = false;
    }
}
