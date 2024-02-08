using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public Camera cam;
    public float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Allows camera movement from player input
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        // Calculating X and Y axis rotation
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        // Setting bounds to rotation
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        // Apply to camera transform
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // Rotate player to look in directions
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
