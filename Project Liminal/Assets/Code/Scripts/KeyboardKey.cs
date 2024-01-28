using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardKey : MonoBehaviour
{

    [Header("Audio Settings")]
    public AudioController audioController;

    public bool isPressed = false;
    public float maxPressedDistance = 0.1f;
    public float lerpSpeed = 5f;

    private Vector3 targetPosition;
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Set the original position
        originalPosition = transform.position;
        targetPosition = new Vector3(transform.position.x, transform.position.y - maxPressedDistance, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            
            // Lerp the position to the key to be clicked
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);

            // Check if the key has arrived at the max pressed position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                // Come up and disable isPressed
                isPressed = false;

                audioController.PlayAudio("key"); // Play the key sound
            }
        }
        else
        {
            // Lerp the position back to the original position
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * lerpSpeed);
        }
    }
    //Debug.Log("isPressed: " + isPressed);
}
