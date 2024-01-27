using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnHit : MonoBehaviour
{
    [SerializeField, Tooltip("The name of the SFX that should play on hit.")] private string sfxName;
    void OnCollisionEnter(Collision collision) => AudioManager.Instance.PlaySfx(sfxName);
}
