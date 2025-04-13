using System.Collections;
using System.Collections.Generic;
using Es.InkPainter.Sample;
using UnityEngine;
using UnityEngine.UI;

public class ColorPanelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private MousePainter mousePainter;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private Transform panelTransform;

    [Header("Options")]
    [SerializeField]
    private List<Color> colors;

    private void AddButton(Color color)
    {
        GameObject newButtonObject = Instantiate(buttonPrefab, panelTransform);

        // Set the color of the new button
        Button newButton = newButtonObject.GetComponent<Button>();
        newButton.image.color = color;

        // Give the button access to mousePainter
        SetColorButtonScript buttonScript = newButton.GetComponent<SetColorButtonScript>();
        if (buttonScript != null)
        {
            buttonScript.mousePainter = mousePainter;
        }
    }

    void Start()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Color color in colors)
        {
            AddButton(color);
        }
    }
}
