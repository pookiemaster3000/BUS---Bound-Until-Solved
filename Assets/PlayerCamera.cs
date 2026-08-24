using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
     public float sensitivityX;
     public float sensitivityY;

     public Transform oreintation;

     float xRotation;
     float yRotation;

private void start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

private void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        // Calculate rotation
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotation to camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        oreintation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}