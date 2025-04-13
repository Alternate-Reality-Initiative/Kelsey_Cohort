using UnityEngine;
using UnityEngine.UI;

public class ToggleObject : MonoBehaviour
{
    public GameObject targetObject; // Assign the object you want to toggle
    private bool isActive = false;

    void Start()
    {
        // Ensure the button has an onClick event assigned
        GetComponent<Button>().onClick.AddListener(ToggleActive);
    }

    void ToggleActive()
    {
        isActive = !isActive; // Toggle state
        targetObject.SetActive(isActive); // Apply state
    }
}
