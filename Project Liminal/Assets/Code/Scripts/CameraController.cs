using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target; // The target object to follow

    void Update()
    {
        // If the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits a GameObject
            if (Physics.Raycast(ray, out hit))
            {
                // If the hit GameObject is the current target, release the camera
                if (hit.transform.gameObject == target)
                {
                    target = null;
                }
                // Otherwise, set the hit GameObject as the target
                else
                {
                    target = hit.transform.gameObject;
                }
            }
        }

        // If a target is assigned, make the camera look at the target
        if (target != null)
        {
            transform.LookAt(target.transform);
        }
    }
}