using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI: MonoBehaviour {
    public Animator playerAnimator;

    void Start() {
        playerAnimator.SetFloat("drunkedness", 1f);
    }
}
