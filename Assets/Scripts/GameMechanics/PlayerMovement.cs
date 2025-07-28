using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;
    public float jumpForceMagnitude = 5f;
    public float mouseSensitivity = 5f;

    private Rigidbody playerRigidBody;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();

        // player won't rotate along any horizontal axis
        playerRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        Cursor.visible = false;
    }

    void Update()
    {
        this.handlePlayerRotation();
        this.handlePlayerJump();
    }

    void FixedUpdate()
    {
        this.handlePlayerMovement();
    }

    private void handlePlayerMovement()
    {
        Vector3 movementDirectionVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movementDirectionVector += this.transform.forward;

        if (Input.GetKey(KeyCode.A))
            movementDirectionVector += -this.transform.right;

        if (Input.GetKey(KeyCode.S))
            movementDirectionVector += -this.transform.forward;

        if (Input.GetKey(KeyCode.D))
            movementDirectionVector += this.transform.right;

        movementDirectionVector = movementDirectionVector.normalized * Time.fixedDeltaTime * movementSpeed;
        playerRigidBody.MovePosition(playerRigidBody.position + movementDirectionVector);
    }

    private void handlePlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && this.isAtTheGround())
        {
            playerRigidBody.AddForce(Vector3.up * jumpForceMagnitude, ForceMode.VelocityChange);
        }
    }

    private void handlePlayerRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(mouseSensitivity * mouseX * Vector3.up); // rotation through Y axis
    }

    private bool isAtTheGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
