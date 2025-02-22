using System.Collections;
using System.Collections.Generic;
using Es.InkPainter.Sample;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorScript : MonoBehaviour
{
    public Button myButton;
    public MousePainter mousePainter;

    private void Start() {
        if (myButton != null && mousePainter != null)
        {
            myButton.onClick.AddListener(() => mousePainter.GetBrush().Color = Color.red);
        }
    }
}
