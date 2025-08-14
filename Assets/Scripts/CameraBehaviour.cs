using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject targetToFollow;
    public Camera mainCamera;

    public float horizontalMouseSensitivity = 5f;
    public float verticalMouseSensitivity = 5f;
    public float fieldOfView = 90f;

    private float yaw; // horizontal rotation (left/right)
    private float pitch; // vertical rotation (up/down)
    private Vector3 cameraOffset;

    void Start()
    {
        // initializing private attributes
        this.yaw = 0f;
        this.pitch = 0f;
        this.cameraOffset = new Vector3(0f, 0.5f, 0f);

        // setting camera FOV
        mainCamera.fieldOfView = fieldOfView;

        // locking and hiding the cursor 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * horizontalMouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * verticalMouseSensitivity;

        // Update yaw and pitch
        this.yaw += mouseX;
        this.pitch -= mouseY;

        // Clamp pitch to avoid flipping
        this.pitch = Mathf.Clamp(this.pitch, -80f, 80f);

        // Position and Rotate Camera
        mainCamera.transform.SetPositionAndRotation(
            targetToFollow.transform.position + cameraOffset, Quaternion.Euler(this.pitch, this.yaw, 0f));
    }
}
    