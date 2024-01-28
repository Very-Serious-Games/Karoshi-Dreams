using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // Add this line

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color defaultColor = Color.white; // Color when not hovering
    public Color hoverColor = Color.yellow; // Color when hovering

    private TextMeshProUGUI buttonText; // Change this line

    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>(); // And this line
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the color of the text to the hover color
        buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Change the color of the text back to the default color
        buttonText.color = defaultColor;
    }
}