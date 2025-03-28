using System.Collections;
using System.Collections.Generic;
using Es.InkPainter.Sample;
using UnityEngine;
using UnityEngine.UI;

public class SetColorButtonScript : MonoBehaviour
{
    private Button button;

    public MousePainter mousePainter;

    private void Start()
    {
        button = GetComponent<Button>();

        if (button != null && mousePainter != null)
        {
            button.onClick.AddListener(() =>
            {
                mousePainter.GetCurrentBrush().Color = button.image.color;
            });
        }
    }
}
