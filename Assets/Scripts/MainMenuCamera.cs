using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCamera : MonoBehaviour
{
    public float bobbingSpeed = 1.0f;  // Adjust the speed of the bobbing motion
    public float bobbingAmount = 0.5f; // Adjust the amount of bobbing motion
    public float rotationSpeed = 1.0f;

    public float startRotation = -10f;
    public float endRotation = 60f;
    public float rotationTime = 1.5f;

    private bool isRotating = false;
    private float elapsedTime = 0f;


    public GameObject prompt;

    private float originalY;

    void Start()
    {
        originalY = transform.position.y;
    }

    void Update()
    {
        float newY = originalY + Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
        {
            prompt.SetActive(false);
            StartRotation();
        }

        if (isRotating)
        {
            RotateObjectOverTime();
        }
    }

    void StartRotation()
    {
        isRotating = true;
        elapsedTime = 0f;
    }

    void RotateObjectOverTime()
    {
        elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(elapsedTime / rotationTime);
        float currentRotation = Mathf.Lerp(startRotation, endRotation, t);

        // Rotate the object around the x-axis
        transform.rotation = Quaternion.Euler(currentRotation, 0f, 0f);

        if (t >= 1.0f)
        {
            isRotating = false;
            SceneManager.LoadScene(1);
        }
    }
}
