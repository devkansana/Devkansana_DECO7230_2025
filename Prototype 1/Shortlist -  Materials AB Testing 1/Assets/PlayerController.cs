using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    public float speed = 5f;                 // Movement speed
    public float mouseSensitivity = 100f;   // Mouse sensitivity
    public Transform playerCamera;           // Assign the child Camera here

    float xRotation = 0f;

    void Start()
    {
        // Lock the cursor to the game view
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Movement input
        float moveX = Input.GetAxis("Horizontal"); // A, D or Left/Right arrows
        float moveZ = Input.GetAxis("Vertical");   // W, S or Up/Down arrows

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * speed * Time.deltaTime;
    }
}
