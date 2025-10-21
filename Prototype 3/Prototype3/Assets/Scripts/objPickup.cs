using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Transform playerCamera;
    private bool isPickedUp = false;
    private Transform originalParent;
    private float pickUpDistance = 3f;

    private float rotationSpeed = 5f; // Adjust this for sensitivity

    void Start()
    {
        playerCamera = Camera.main.transform;
        originalParent = transform.parent;
    }

    void Update()
    {
        // On left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            if (!isPickedUp)
            {
                TryPickUp();
            }
            else
            {
                Drop();
            }
        }

        if (isPickedUp)
        {
            RotatePickedUpObject();
        }
    }

    void TryPickUp()
    {
        Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickUpDistance))
        {
            if (hit.transform == this.transform)
            {
                isPickedUp = true;
                transform.SetParent(playerCamera);
                transform.localPosition = new Vector3(0, 0, 2);
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    void Drop()
    {
        isPickedUp = false;
        transform.SetParent(originalParent);
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void RotatePickedUpObject()
    {
        // Hold right mouse and move to rotate
        if (Input.GetMouseButton(1)) // Right mouse button held
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Apply X rotation around up axis (yaw), Y rotation around right axis (pitch)
            transform.Rotate(playerCamera.up, -rotX, Space.World);
            transform.Rotate(playerCamera.right, rotY, Space.World);
        }
    }
}
