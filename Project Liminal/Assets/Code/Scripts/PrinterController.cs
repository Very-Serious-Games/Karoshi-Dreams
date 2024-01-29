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
        /*
        GameObject obj = Instantiate(paperPrefab, transform.position, transform.rotation);
        TextMeshProUGUI text = obj.folioText;
        text.text = screenText.text;
        screenText.text = "";
        */
    }
}
