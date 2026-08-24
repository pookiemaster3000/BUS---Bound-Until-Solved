using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        // Move the camera to the specified position
        transform.position = cameraPosition.position;
    }
}