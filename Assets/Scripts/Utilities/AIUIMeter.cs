using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// AIUIMeter class represents a UI meter component in Unity.
public class AIUIMeter : MonoBehaviour
{
    // Serialized fields for inspector visibility.
    [SerializeField] TMP_Text label;   // Text element for displaying additional information.
    [SerializeField] Slider slider;     // Slider for representing a numerical value.
    [SerializeField] Image image;       // Image for visual representation.

    // Property for setting the position of the meter in world space.
    public Vector3 position
    {
        set
        {
            // Draw a debug line from the given position upward for visualization.
            Debug.DrawLine(value, value + Vector3.up * 3);

            // Convert the world space position to a viewport point.
            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(value);

            // Set the anchor points of the RectTransform to match the converted viewport point.
            GetComponent<RectTransform>().anchorMin = viewportPoint;
            GetComponent<RectTransform>().anchorMax = viewportPoint;
        }
    }

    // Property for setting the numerical value of the meter.
    public float value
    {
        set
        {
            // Set the value of the slider to represent the specified numerical value.
            slider.value = value;
        }
    }

    // Property for setting the text displayed on the meter.
    public string text
    {
        set
        {
            // Set the text value of the label element to the specified string.
            label.text = value;
        }
    }

    // Property for toggling the visibility of the meter.
    public bool visible
    {
        set
        {
            // Set the active state of the GameObject to show or hide the meter.
            gameObject.SetActive(value);
        }
    }

    // Property for setting the alpha (transparency) of the meter's image.
    public float alpha
    {
        set
        {
            // Get the current color of the image.
            Color color = image.color;

            // Update the alpha component of the color to the specified value.
            color.a = value;

            // Set the updated color back to the image.
            image.color = color;
        }
    }
}