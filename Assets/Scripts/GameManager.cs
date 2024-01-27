using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public List<GuardController> guards;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }
}
