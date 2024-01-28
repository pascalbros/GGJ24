using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HiccupManager: MonoBehaviour {

    public static HiccupManager Instance { get; private set; }

    public HiccupBar hiccupBar;
    public float maxHiccupGuardDistance;
    [SerializeField] float hiccupStrength = 1f;
    [SerializeField] float hiccupTime = 0.3f;

    [SerializeField] float burpStrength = 1f;
    [SerializeField] float burpTime = 0.3f;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    private void FixedUpdate() {
        var playerPosition = PlayerController.Instance.transform.position;
        foreach (var guard in GameManager.Instance.guards) {
            guard.PlayerIsNearby(Vector3.Distance(playerPosition, guard.transform.position) <= maxHiccupGuardDistance);
        }
    }

    public void OnHiccup() {
        var playerPosition = PlayerController.Instance.transform.position;
        foreach (var guard in GameManager.Instance.guards) {
            if (Vector3.Distance(playerPosition, guard.transform.position) <= maxHiccupGuardDistance) {
                var sequence = DOTween.Sequence();
                sequence.AppendInterval(0.35f);
                sequence.AppendCallback(() => guard.OnHiccupNearby(playerPosition));
                sequence.Play();
            }
        }
        AudioManager.Instance.PlaySfx("hiccup-1");
        CameraManager.Instance.ShakeCamera(hiccupStrength, hiccupTime);
    }

    public void OnBurp() {
        var playerPosition = PlayerController.Instance.transform.position;
        foreach (var guard in GameManager.Instance.guards) {
            if (Vector3.Distance(playerPosition, guard.transform.position) <= maxHiccupGuardDistance) {
                var sequence = DOTween.Sequence();
                sequence.AppendInterval(0.35f);
                sequence.AppendCallback(() => guard.OnBurpNearby(playerPosition));
                sequence.Play();
            }
        }
        AudioManager.Instance.PlaySfx("hiccup-1");
        CameraManager.Instance.ShakeCamera(burpStrength, burpTime);
    }
}
