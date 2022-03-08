using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandle : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    public Animator animator;

    public Light lighting;
    private bool lighton = false;

    #region MouseMovement
    void Start()
    {
        // Locks the cursor so that it does not show on the screen.

        Cursor.lockState = CursorLockMode.Locked;
    }

    

    void Update()
    {
        // Moves the camera according to the mouse. Rotates the player character accordingly.

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 66.56f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            FlashLight();
        }

    }

    // Turns on the flashlight.

    void FlashLight()
    {
        if (lighton == false)
        {
            lighton = true;
            lighting.enabled = true;
        }
        else if (lighton == true)
        {
            lighton = false;
            lighting.enabled = false;
        }
    }
    #endregion



}
