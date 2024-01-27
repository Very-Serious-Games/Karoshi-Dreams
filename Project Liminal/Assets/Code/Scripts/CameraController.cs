using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target; // The target object to follow

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // If the clicked object is the current target, release it
                if (hit.transform.gameObject == target)
                {
                    target = null;
                }
                else
                {
                    target = hit.transform.gameObject;
                }
            }
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
        }
    }
}