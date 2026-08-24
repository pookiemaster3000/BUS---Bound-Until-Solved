using UnityEngine;
using TMPro;
using System.Collections;

public class Interactable : MonoBehaviour
{
    [Header("Interaction")]
    [TextArea]
    public string interactionMessage = "silly! you don't need this yet";

    [Header("UI")]
    public TextMeshProUGUI messageText;

    [Header("Shake Settings")]
    public float shakeAmount = 0.1f;
    public float shakeDuration = 0.3f;

    private Vector3 originalPosition;
    private bool isInteracting = false;

    public virtual void Interact()
    {
        if (!isInteracting)
        {
            StartCoroutine(InteractRoutine());
        }
    }

    private IEnumerator InteractRoutine()
    {
        isInteracting = true;

        originalPosition = transform.localPosition;

        // Display the message
        messageText.text = interactionMessage;

        // Shake the object
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-shakeAmount, shakeAmount);
            float y = Random.Range(-shakeAmount, shakeAmount);
            float z = Random.Range(-shakeAmount, shakeAmount);

            transform.localPosition = originalPosition + new Vector3(x, y, z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Put the object back exactly where it started
        transform.localPosition = originalPosition;

        isInteracting = false;
    }
}