using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance;

    private AudioSourcePool pool;

    public bool Invulnarable;

    void Awake() {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start() {
        pool = FindObjectOfType<AudioSourcePool>();
    }

    void Update () {
		
	}

    public void win() {
        UIController.instance.showWinScreen();
        pool.gameObject.SetActive(false);
        Invoke("restartScene", 2);
    }

    public void lose() {
        UIController.instance.showLoseScreen();
        pool.gameObject.SetActive(false);
        Invoke("restartScene", 2);
    }

    private void restartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
