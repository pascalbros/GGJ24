using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class MenuUI: MonoBehaviour {
    public Animator playerAnimator;
    public GameObject fade;

    void Start() {
        playerAnimator.SetFloat("drunkedness", 1f);
        AudioManager.Instance.PlayMusic("main-theme");
        InputSystem.onAnyButtonPress.CallOnce(ctrl => {
            fade.SetActive(true);
        });
    }
}
