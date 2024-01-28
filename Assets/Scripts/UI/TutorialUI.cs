using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class TutorialUI: MonoBehaviour {

    static bool shown = false;
    public CanvasGroup canvas;

    private void Start() {
        if (!shown) {
            Appear();
            shown = true;
        }
    }

    public void Appear() {
        InputSystem.onAnyButtonPress.CallOnce(ctrl => {
            Hide();
        });
    }

    public void Hide() {
        canvas.DOFade(0f, 1f).OnComplete(() => {
            gameObject.SetActive(false);
            PlayerController.Instance.state = PlayerController.State.IN_GAME;
        });
    }
}
