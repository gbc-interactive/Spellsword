using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class ParticleScreenShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeAmplitude = 1.2f;
    public float shakeFrequency = 2.0f;

    private float shakeTimer = 0f;

    // Reference to the virtual camera
    public CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        if (virtualCamera != null)
        {
            // ... (Same initialization code as before)
        }
        else
        {
            Debug.LogError("CinemachineVirtualCamera not assigned to the ParticleScreenShake script.");
        }
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            // Decrement the timer
            shakeTimer -= Time.deltaTime;
        }
    }

    // Call this method to trigger the screen shake
    public void StartScreenShake()
    {
        // Trigger the screen shake using CinemachineImpulseSource
        if (virtualCamera != null)
        {
            CinemachineImpulseSource impulseSource = virtualCamera.GetComponent<CinemachineImpulseSource>();
            if (impulseSource != null)
            {
                // Set the CinemachineImpulseSource settings
                impulseSource.m_ImpulseDefinition.m_AmplitudeGain = shakeAmplitude;
                impulseSource.m_ImpulseDefinition.m_FrequencyGain = shakeFrequency;

                // Generate the impulse
                impulseSource.GenerateImpulse();

                // Set the shake timer to the duration
                shakeTimer = shakeDuration;
            }
            else
            {
                Debug.LogError("CinemachineImpulseSource component not found on the virtual camera.");
            }
        }
    }
}

