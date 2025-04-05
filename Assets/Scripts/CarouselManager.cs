using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CarouselManager : MonoBehaviour
{
    private InputAction mouseMoveAction;
    private bool _isSwiping = false;
    private int last_index = 0;
    public int start_index = 0;
    public CanvasGroup ui_elements;
    private List<GameObject> carousel_objects = new List<GameObject>();
    private List<Sprite> carousel_sprites = new List<Sprite>();

    private void Start()
    {
        mouseMoveAction = new InputAction(type: InputActionType.Value, binding: "<Pointer>/delta");
        mouseMoveAction.Enable();
        foreach (Transform childTransform in transform)
        {
            GameObject child = childTransform.gameObject;
            carousel_objects.Add(child);
            child.SetActive(false);
            Sprite child_sprite = Resources.Load<Sprite>("Sprites/" + child.name);
            carousel_sprites.Add(child_sprite);
        }
        carousel_objects[0].SetActive(true);
        InputAction clickAction = new InputAction(binding: "<Mouse>/leftButton");
        clickAction.performed += OnMouseClicked;
        clickAction.Enable();
        // do UI stuff
        set_ui_elements();
    }

    void set_ui_elements()
    {
        if (ui_elements != null)
        {
            int left_index = start_index + 1, right_index = start_index - 1;
            if (right_index < 0) { right_index = carousel_objects.Count - 1; }
            if (left_index >= carousel_objects.Count) { left_index = 0; }
            Dictionary<string, int> new_ui_elts = new Dictionary<string, int>();
            new_ui_elts["Left"] = left_index;
            new_ui_elts["Center"] = start_index;
            new_ui_elts["Right"] = right_index;
            foreach (Transform child in ui_elements.transform)
            {
                // Check if the child's name matches the searched name
                if (new_ui_elts.ContainsKey(child.name))
                {
                    // Return the Image component if found
                    child.GetComponent<Image>().sprite = carousel_sprites[new_ui_elts[child.name]];
                }
            }
        }
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed && !_isSwiping)
        {
            float x = mouseMoveAction.ReadValue<Vector2>().x;
            if (x < 0)
            {
                start_index += 1;
                if (start_index >= carousel_objects.Count)
                {
                    start_index = 0;
                }
                _isSwiping = true;
            }
            else if (x > 0)
            {
                start_index -= 1;
                if (start_index < 0)
                {
                    start_index = carousel_objects.Count - 1;
                }
                _isSwiping = true;
            }
        }
        if (!Mouse.current.leftButton.isPressed)
        {
            _isSwiping = false;
        }
        if (!_isSwiping && start_index != last_index)
        {
            Debug.Log("Now showing item index: " + start_index);
            carousel_objects[start_index].SetActive(true);
            carousel_objects[last_index].SetActive(false);
            last_index = start_index;
            set_ui_elements();
        }
    }
    private void OnMouseClicked(InputAction.CallbackContext context)
    {
        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        // Store hit information
        RaycastHit hitInfo;

        // Perform the raycast
        if (Physics.Raycast(ray, out hitInfo))
        {
            // Get the GameObject hit by the ray
            GameObject hitObject = hitInfo.collider.gameObject;

            Transform parentTransform = hitObject.transform.parent;
            if (parentTransform != null)
            {
                if (parentTransform == transform)
                {
                    Debug.Log("Do something on click ig");
                }
            }
            else
            {
                Debug.Log("Hit object has no parent.");
            }

        }
    }
}
