using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackout : MonoBehaviour
{

    public Shader currentShader = null;
    private Material currentMaterial = null;

    public float Timer;
    public float Pause = 1f;

    public float Rate = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        if (!CheckResources()) {
            Graphics.Blit(source, destination);
            return;
        }

        currentMaterial.SetFloat("_Timer", Timer);

        Graphics.Blit(source, destination, currentMaterial);
    }

    private bool CheckResources() {
        currentShader = Shader.Find("Blackout");
        currentMaterial = new Material(currentShader);
        return true;
    }

    public void StartEffect() {
        StopAllCoroutines();
        StartCoroutine(blackoutCo());
    }

    public void StopEffect() {
        StopAllCoroutines();
    }

    private IEnumerator blackoutCo() {
        enabled = true;
        do {
            Timer += Rate;
            yield return new WaitForSeconds(Rate);
        } while (Timer < 1);
        yield return new WaitForSeconds(Pause);
        do {
            Timer -= Rate;
            yield return new WaitForSeconds(Rate);
        } while (Timer > 0);

        enabled = false;
    }
}
