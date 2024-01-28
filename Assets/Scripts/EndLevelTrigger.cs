using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger: MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) {
            OnEndLevel();
        }
    }

    private void OnEndLevel() {
        GameManager.Instance.OnPlayerWin();
    }
}
