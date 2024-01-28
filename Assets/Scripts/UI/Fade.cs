using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade: MonoBehaviour {
    public string levelName;

    public GameObject fadeIn;
    public GameObject fadeOut;
    public bool isFadeIn = true;
    void Start() {
        fadeIn.SetActive(isFadeIn);
        fadeOut.SetActive(!isFadeIn);
    }

    public void OnFadeFinished(string value) {
        if (isFadeIn) {
            Destroy(gameObject);
        } else {
            SceneManager.LoadScene(levelName);
        }
    }
}
