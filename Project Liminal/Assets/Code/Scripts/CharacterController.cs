using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Camera playerCamera;

    [Header("Target Settings")]
    public GameObject target; // The target object to follow

    [Header("Mouse Settings")]
    public float mouseSensitivity = 600.0f;
    public float clampAngleX = 80.0f;
    public float clampAngleY = 90.0f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 20f; // Adjust this value to change the speed of rotation
    public float releaseDuration = 0.1f; // Duration of the smooth release

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis
    private bool isLockedOnTarget = false; // Whether the camera is locked on the target
    private Coroutine releaseCoroutine;

    void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 rot = playerCamera.transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Update()
    {
        // Movement
        # region Movement
        if (!isLockedOnTarget)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            rotY += mouseX * mouseSensitivity * Time.deltaTime;
            rotX += mouseY * mouseSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngleX, clampAngleX);
            rotY = Mathf.Clamp(rotY, -clampAngleY, clampAngleY);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            playerCamera.transform.rotation = localRotation;
        }
        # endregion

        // Raycast
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // If the clicked object is the current target, release it
                if (hit.transform.gameObject == target)
                {
                    if (releaseCoroutine != null)
                    {
                        StopCoroutine(releaseCoroutine);
                    }
                    releaseCoroutine = StartCoroutine(ReleaseTarget());
                }
                else
                {
                    target = hit.transform.gameObject;
                    isLockedOnTarget = true;
                }
            }
        }
    }

    void LateUpdate()
    {
        if (target != null && isLockedOnTarget)
        {
            Vector3 direction = (target.transform.position - playerCamera.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the target
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator ReleaseTarget()
    {
        float elapsed = 0f;
        Quaternion initialRotation = playerCamera.transform.rotation;
        while (elapsed < releaseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / releaseDuration;

            Vector3 direction = (target.transform.position - playerCamera.transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion freeRotation = Quaternion.Euler(rotX, rotY, 0.0f);

            playerCamera.transform.rotation = Quaternion.Slerp(targetRotation, freeRotation, t);

            yield return null;
        }

        target = null;
        isLockedOnTarget = false;
    }
}