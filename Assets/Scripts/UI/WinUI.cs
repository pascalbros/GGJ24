using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class WinUI: MonoBehaviour {

    public string nextSceneName;
    public CanvasGroup canvas;
    public TextMeshProUGUI continueText;

    public void Appear() {
        gameObject.SetActive(true);
        canvas.DOFade(1f, 1f).OnComplete(() => {
            continueText.DOFade(1f, 1f).OnComplete(() => {
                InputSystem.onAnyButtonPress.CallOnce(ctrl => {
                    GameManager.Instance.FadeOut(nextSceneName);
                });
            });
        });
    }
}