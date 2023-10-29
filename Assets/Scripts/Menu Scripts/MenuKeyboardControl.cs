using UnityEngine;
using UnityEngine.EventSystems; // Required for EventSystem

public class MenuKeyboardControl : MonoBehaviour
{
    private EventSystem eventSystem;

    void Start()
    {
        eventSystem = EventSystem.current; // Get the current Event System
    }

    void Update()
    {
        // Check for the Enter key press
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Execute the current selected button's click event
            var currentButton = eventSystem.currentSelectedGameObject.GetComponent<UnityEngine.UI.Button>();
            if (currentButton)
            {
                currentButton.onClick.Invoke();
            }
        }

        // Optional: If no button is selected, select the first one
        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        }
    }
}

