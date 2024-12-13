// Edited by Sofiya
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private void Awake()
    {
        // Ensure the cursor is visible and not restricted
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
