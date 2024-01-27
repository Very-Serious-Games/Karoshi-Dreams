using System.Collections.Generic;
using UnityEngine;

public class OutlineOnMouseOver : MonoBehaviour
{
    public Material outlineMaterial;
    private Dictionary<GameObject, Material[]> originalMaterials = new Dictionary<GameObject, Material[]>();
    private GameObject lastHitObject;

    private void Update()
    {
        // Get the ray from the camera to the mouse position
        Vector2 middleScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(middleScreen);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit GameObject has the "Lockable" tag
            if (hit.transform.gameObject.CompareTag("Lockable"))
            {
                // If a different GameObject was hit, restore the materials of the previously hit GameObject
                if (lastHitObject != null && lastHitObject != hit.transform.gameObject)
                {
                    lastHitObject.GetComponent<Renderer>().materials = originalMaterials[lastHitObject];
                }

                // Store the original materials of the hit GameObject if they haven't been stored yet
                if (!originalMaterials.ContainsKey(hit.transform.gameObject))
                {
                    originalMaterials[hit.transform.gameObject] = hit.transform.gameObject.GetComponent<Renderer>().materials;
                }

                // Create a new array of materials that includes the original materials and the outline material
                List<Material> materials = new List<Material>(originalMaterials[hit.transform.gameObject]);
                materials.Add(outlineMaterial);
                hit.transform.gameObject.GetComponent<Renderer>().materials = materials.ToArray();

                // Store the hit GameObject for the next frame
                lastHitObject = hit.transform.gameObject;
            }
            else if (lastHitObject != null)
            {
                // If the raycast did not hit a GameObject with the "Lockable" tag, restore the materials of the previously hit GameObject
                lastHitObject.GetComponent<Renderer>().materials = originalMaterials[lastHitObject];
                lastHitObject = null;
            }
        }
        else if (lastHitObject != null)
        {
            // If the raycast did not hit any GameObject, restore the materials of the previously hit GameObject
            lastHitObject.GetComponent<Renderer>().materials = originalMaterials[lastHitObject];
            lastHitObject = null;
        }
    }
}