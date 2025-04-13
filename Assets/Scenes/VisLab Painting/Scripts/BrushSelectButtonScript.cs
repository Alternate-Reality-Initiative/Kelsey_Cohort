using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushSelectButtonScript : MonoBehaviour
{
    private Button button;

    public int brushIndex;

    [SerializeField]
    private MousePainter mousePainter;


    void Start()
    {
        button = GetComponent<Button>();

        if (button != null && mousePainter != null)
        {
            button.onClick.AddListener(() => mousePainter.ChangeBrush(brushIndex));
        }
    }
}
