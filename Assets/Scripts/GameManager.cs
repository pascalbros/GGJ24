using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public List<GuardController> guards;
    public BustedUI bustedUI;
    public WinUI winUI;
    public GameObject fade;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public void DisableGuards() {
        foreach (var guard in guards) {
            guard.DisableGuard();
        }
    }

    public void OnPlayerBusted() {
        DisableGuards();
        PlayerController.Instance.OnBusted();
        bustedUI.Appear();
    }

    public void OnPlayerWin() {
        DisableGuards();
        PlayerController.Instance.OnWin();
        winUI.Appear();
    }

    public void FadeOut(string levelName) {
        var obj = Instantiate(fade);
        var fadeObj = obj.GetComponent<Fade>();
        fadeObj.levelName = levelName;
        fadeObj.isFadeIn = false;
        obj.SetActive(true);
    }
}
