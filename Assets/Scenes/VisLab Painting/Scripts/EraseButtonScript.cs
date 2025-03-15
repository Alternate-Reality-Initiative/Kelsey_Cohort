using System.Collections;
using System.Collections.Generic;
using Es.InkPainter.Sample;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EraseButtonScript : MonoBehaviour
{
    private Button button;

    public MousePainter mousePainter;

    void Start()
    {
        button = GetComponent<Button>();

        if (button != null && mousePainter != null)
        {
            button.onClick.AddListener(() => mousePainter.SetErase(true));
        }
    }

}
