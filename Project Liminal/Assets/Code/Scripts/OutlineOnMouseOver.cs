using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineOnMouseOver : MonoBehaviour
{
    public Material normalMaterial;
    public Material outlineMaterial;
    private Renderer renderer;
    private GameObject lastHitObject;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // Get the ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit GameObject has the "Lockable" tag
            if (hit.transform.gameObject.CompareTag("Lockable"))
            {
                // If a different GameObject was hit, change the material of the previously hit GameObject back to the normal material
                if (lastHitObject != null && lastHitObject != hit.transform.gameObject)
                {
                    lastHitObject.GetComponent<Renderer>().material = normalMaterial;
                }

                // Change the material of the hit GameObject to the outline material
                hit.transform.gameObject.GetComponent<Renderer>().material = outlineMaterial;

                // Store the hit GameObject for the next frame
                lastHitObject = hit.transform.gameObject;
            }
            else if (lastHitObject != null)
            {
                // If the raycast did not hit a GameObject with the "Lockable" tag, change the material of the previously hit GameObject back to the normal material
                lastHitObject.GetComponent<Renderer>().material = normalMaterial;
                lastHitObject = null;
            }
        }
    }
}