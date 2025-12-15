using UnityEngine;
using UnityEngine.InputSystem; // You need the Input System Package

public class ARInputHandler : MonoBehaviour
{
    void Update()
    {
        // Check for screen tap/touch input
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            // Raycast from the touch position
            RaycastHit hit;
            // Get the touch position and convert it to a screen ray
            Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());

            // Cast the ray and check if it hits a collider
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has the ARInteractable script
                ARInteractable interactable = hit.collider.GetComponentInParent<ARInteractable>();

                if (interactable != null)
                {
                    // Call the OnTap function on the script
                    interactable.OnTap();
                }
            }
        }
    }
}