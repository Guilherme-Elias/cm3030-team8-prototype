using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera; // player follows main camera's Y axis rotation
    public Rigidbody player;

    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;
    private float jumpForceMagnitude = 5f;
    public float mouseSensitivity = 5f;

    void Start()
    {
        // player won't rotate by itself
        player.constraints = RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        this.HandlePlayerRotation();
        this.HandlePlayerJump();            
    }

    void FixedUpdate()
    {
        this.HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        Vector3 movementDirectionVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movementDirectionVector += player.transform.forward;

        if (Input.GetKey(KeyCode.A))
            movementDirectionVector += -player.transform.right;

        if (Input.GetKey(KeyCode.S))
            movementDirectionVector += -player.transform.forward;

        if (Input.GetKey(KeyCode.D))
            movementDirectionVector += player.transform.right;

        movementDirectionVector = movementSpeed * Time.fixedDeltaTime * movementDirectionVector.normalized;
        player.MovePosition(player.position + movementDirectionVector);
    }

    private void HandlePlayerJump()
    {

        if(Input.GetKeyDown(KeyCode.Space) && IsAtTheGround())
        {
            player.AddForce(Vector3.up * jumpForceMagnitude, ForceMode.VelocityChange);
        }

    }

    private void HandlePlayerRotation()
    {
        float cameraYaw = mainCamera.transform.eulerAngles.y;
        player.rotation = Quaternion.Euler(0f, cameraYaw, 0f);
    }

    private bool IsAtTheGround()
    {
        bool grounded = Physics.Raycast(player.transform.position, Vector3.down, 1.1f);
        return grounded;
    }
}
