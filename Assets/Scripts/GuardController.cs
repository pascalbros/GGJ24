using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuardController: MonoBehaviour {

    public FieldOfViewController fovController;

    public Color playerNotInRangeColor;
    public Color playerInRangeColor;
    public float fovAnimationDuration = 0.2f;

    void Start() {
        fovController.onTargetStatusChanged += OnPlayerSighted;
    }

    private void OnPlayerSighted(bool sighted) {
        Debug.Log(sighted);
        var color = sighted ? playerInRangeColor : playerNotInRangeColor;
        fovController.AnimateToColor(color, fovAnimationDuration);
    }
}
