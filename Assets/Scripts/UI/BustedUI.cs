using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class BustedUI: MonoBehaviour {

    public CanvasGroup canvas;
    public TextMeshProUGUI continueText;

    public void Appear() {
        gameObject.SetActive(true);
        canvas.DOFade(1f, 3f).OnComplete(() => {
            continueText.DOFade(1f, 1f).OnComplete(() => {
                InputSystem.onAnyButtonPress.CallOnce(ctrl => {
                    Scene scene = SceneManager.GetActiveScene();
                    GameManager.Instance.FadeOut(scene.name);
                });
            });
        });
    }
}
