using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrinterController : MonoBehaviour
{
    public GameObject paperPrefab;

    public GameObject textContainer;
    private TextMeshProUGUI screenText;

    private TextMeshProUGUI thisText;

    // Start is called before the first frame update
    void Start()
    {
        screenText = textContainer.GetComponent<TextMeshProUGUI>();
        thisText = paperPrefab.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Print()
    {
        GameObject obj = Instantiate(paperPrefab, transform.position, transform.rotation);
        Transform textChild = obj.transform.Find("Text (TMP)");
        if (textChild != null)
        {
            TextMeshPro text = textChild.GetComponent<TextMeshPro>();
            if (text != null)
            {
                text.text = screenText.text;
                screenText.text = "";
            }
            else
            {
                Debug.Log("No TextMeshPro component found in the 'Text (TMP)' child.");
            }
        }
        else
        {
            Debug.Log("No child GameObject named 'Text (TMP)' found.");
        }
    }
}
