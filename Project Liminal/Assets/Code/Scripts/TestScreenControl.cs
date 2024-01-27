using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class TestScreenControl : MonoBehaviour
{
    public GameObject image;
    public GameObject canvas;

    public Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, playerCamera, out localPoint);

        localPoint = new Vector2(Input.mousePosition.x / 256, Input.mousePosition.y / 256);

        image.transform.localPosition = localPoint;
    }
}
