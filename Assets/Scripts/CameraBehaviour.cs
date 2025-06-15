using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 cameraOffset = new Vector3(0f, 0.5f, 0f);
    private GameObject targetToFollow;
    private Camera mainCamera;

    public float mouseSensitivity = 5f;
    private float verticalRotation = 0f;

    void Start()
    {
        targetToFollow = GameObject.Find("Player");

        mainCamera = GetComponent<Camera>();
        mainCamera.fieldOfView = 90f;
    }

    void LateUpdate()
    {
        // Update vertical rotation from mouse Y movement
        float mouseY = Input.GetAxis("Mouse Y");
        verticalRotation -= mouseY * mouseSensitivity;

        // Clamp vertical rotation to avoid flipping
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);

        // Position camera relative to player
        this.transform.position = targetToFollow.transform.TransformPoint(cameraOffset);

        // Apply rotation: first vertical (X axis), then horizontal (Y axis from player)
        Vector3 targetEulerAngles = targetToFollow.transform.eulerAngles;
        this.transform.rotation = Quaternion.Euler(verticalRotation, targetEulerAngles.y, 0f);

    }
}
    