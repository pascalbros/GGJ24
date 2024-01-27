using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiccupManager: MonoBehaviour {

    public static HiccupManager Instance { get; private set; }

    public HiccupBar hiccupBar;
    public float maxHiccupGuardDistance;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public void OnHiccup() {
        var playerPosition = PlayerController.Instance.transform.position;
        foreach (var guard in GameManager.Instance.guards) {
            if (Vector3.Distance(playerPosition, guard.transform.position) <= maxHiccupGuardDistance) {
                guard.OnHiccupNearby(playerPosition);
            }
        }
    }
}
