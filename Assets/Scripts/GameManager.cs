using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public List<GuardController> guards;
    public BustedUI bustedUI;
    public WinUI winUI;

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
        PlayerController.Instance.OnWin();
        winUI.Appear();
    }
}
