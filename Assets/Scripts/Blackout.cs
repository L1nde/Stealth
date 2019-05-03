using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Blackout : MonoBehaviour
{

    private Shader currentShader = null;
    public Material currentMaterial = null;

    public float Timer;
    public float Pause = 1f;

    public float Rate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        //if (!CheckResources()) {
          //  Graphics.Blit(source, destination);
            //return;
        //}

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
            Timer += Rate * Time.deltaTime;
            yield return new WaitForSeconds(Rate * Time.deltaTime);
        } while (Timer < 1);
        yield return new WaitForSeconds(Pause);
        do {
            Timer -= Rate * Time.deltaTime;
            yield return new WaitForSeconds(Rate * Time.deltaTime);
        } while (Timer > 0);

        enabled = false;
    }
}
