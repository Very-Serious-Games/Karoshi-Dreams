using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Camera playerCamera;

    public float zoomSpeed = 10f; // Adjust this value to change the speed of zooming
    private float initialFov = 60f; // The initial field of view of the camera
    public float zoomedFov = 30f; // The field of view of the camera when zoomed in

    [Header("Target Settings")]
    public GameObject target; // The target object to follow

    [Header("Mouse Settings")]
    public static float mouseSensitivity = 600.0f;
    public float clampAngleX = 80.0f;
    public float clampAngleY = 90.0f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 20f; // Adjust this value to change the speed of rotation
    public float releaseDuration = 0.1f; // Duration of the smooth release

    [Header("Audio Settings")]
    public AudioController audioController;

    [Header("Scene Settings")]
    public SceneSwitcher sceneSwitcher;

    [Header("Minigame Settings")]
    public MinigameController minigameController;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis
    private bool isLockedOnTarget = false; // Whether the camera is locked on the target
    private Coroutine releaseCoroutine;

    private GameObject tempCube;

    private float elapsedTime = 0f;
    private float speed = 0f;
    private float acceleration = 0.1f;

    void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set initial field of view
        initialFov = playerCamera.fieldOfView;

        // Set initial rotation to look to the front
        rotY = 0.0f;
        rotX = 0.0f;

        // Apply the rotation to the player camera
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        playerCamera.transform.rotation = localRotation;
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

        Vector2 middleScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray= playerCamera.ScreenPointToRay(middleScreen);
        RaycastHit hit;

        // Raycast
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked object has the correct tag
                if (hit.transform.gameObject.CompareTag("Lockable"))
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
                        audioController.PlayAudio("test");
                        sceneSwitcher.SwitchScene("NightmareScene");
                    }
                }
                
                
                if (hit.transform.gameObject.CompareTag("KeyboardKey"))
                {
                    audioController.PlayAudio("key"); // Play the key sound
                    hit.transform.gameObject.GetComponent<KeyboardKey>().isPressed = true;
                }

                if (hit.transform.gameObject.CompareTag("CupWithoutBall")) 
                {
                    // If this cup is clicked, trigger the merge event
                    minigameController.OnCupClicked(hit.transform.gameObject);
                }

                if (hit.transform.gameObject.CompareTag("CupWithBall"))
                {
                    minigameController.OnCupClicked(hit.transform.gameObject);
                }
            }
        }
        else if(Input.GetMouseButton(1))
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }

        if (GlobalVariables.rounds == 5)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime > 10f)
                {
                    speed += acceleration * Time.deltaTime;
                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                    Debug.Log("moviendome hacia arriba jeje");
                    if (elapsedTime > 20f)
                    {
                        Debug.Log("endgame");
                    }
                }
            }
    }

    void ZoomIn()
    {
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomedFov, zoomSpeed * Time.deltaTime);
    }

    void ZoomOut()
    {
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, initialFov, zoomSpeed * Time.deltaTime);
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