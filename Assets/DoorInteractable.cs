using UnityEngine;
using System.Collections;

public class DoorInteractable : Interactable
{
    [Header("Door Settings")]
    public float openAngle = 90f;
    public float openSpeed = 2f;

    private bool isOpen = false;
    private bool isMoving = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    public override void Interact()
    {
        if (!isMoving)
        {
            StartCoroutine(ToggleDoor());
        }
    }

    private IEnumerator ToggleDoor()
    {
        isMoving = true;

        Quaternion targetRotation = isOpen ? closedRotation : openRotation;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                openSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.rotation = targetRotation;

        isOpen = !isOpen;
        isMoving = false;
    }
}