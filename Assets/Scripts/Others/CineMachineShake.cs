using Cinemachine;
using UnityEngine;

public class CineMachineShake : MonoBehaviour
{
    public static CineMachineShake Instance { get; private set; }
    
    public CinemachineVirtualCamera cinemachine;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private void Awake()
    {
        Instance = this;
        cinemachine = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
        cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(startingIntensity, 0f, (1 - (shakeTimer / shakeTimerTotal)));
            }
        }
    }
}
