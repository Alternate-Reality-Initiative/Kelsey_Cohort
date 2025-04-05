using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolManagerScript : MonoBehaviour
{
    [SerializeField]
    private List<Button> toolButtons;

    [SerializeField]
    private float yOffset = 10.0f;
    [SerializeField]
    private float animationDuration = 0.2f;

    private Dictionary<Button, float> startingYs = new Dictionary<Button, float>();

    void Start()
    {
        foreach (var button in toolButtons)
        {
            button.onClick.AddListener(() => OnToolSelected(button));
            startingYs.Add(button, button.GetComponent<RectTransform>().anchoredPosition.y);
        }
    }

    void OnToolSelected(Button clickedButton)
    {
        foreach (var btn in toolButtons)
        {
            StartCoroutine(AnimateButtonY(btn, btn == clickedButton ? yOffset : 0f));
        }
    }

    IEnumerator AnimateButtonY(Button button, float yOffset)
    {
        RectTransform rt = button.GetComponent<RectTransform>();
        Vector2 startPos = rt.anchoredPosition;
        Vector2 targetPos = new Vector2(startPos.x, startingYs[button] + yOffset);

        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);
            rt.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        rt.anchoredPosition = targetPos;
    }
}
