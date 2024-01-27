using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam;

    private float shakeTimer;
    private float shakeTimerTotal;
    private float shakeInitialAmplitude;

    public static CameraManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cameraNoise =
                vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cameraNoise.m_AmplitudeGain =
                Mathf.Lerp(shakeInitialAmplitude, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
    }

    public void ShakeCamera(float amplitude, float time)
    {
        CinemachineBasicMultiChannelPerlin cameraNoise =
                vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cameraNoise.m_AmplitudeGain = amplitude;
        shakeTimer = time;
        shakeTimerTotal = time;
        shakeInitialAmplitude = amplitude;
    }
}