// Winta Edited

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    public float swayAmount;          // How much the object sways in response to mouse movement
    public float swayMax;            // The maximum amount the object can sway
    public float swaySmoothSpeed;    // Speed at which swaying returns to normal

    private Vector3 originalPosition;

    void Start()
    {
        // Save the object's initial local position
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Capture mouse movement and calculate the sway amount
        float swayInputX = -Input.GetAxis("Mouse X") * swayAmount;
        float swayInputY = -Input.GetAxis("Mouse Y") * swayAmount;

        // Clamp the calculated sway values to stay within the defined max range
        swayInputX = Mathf.Clamp(swayInputX, -swayMax, swayMax);
        swayInputY = Mathf.Clamp(swayInputY, -swayMax, swayMax);

        // Set the target position by combining the sway input with the original position
        Vector3 targetSwayPosition = new Vector3(swayInputX, swayInputY, 0) + originalPosition;

        // Smoothly interpolate the object's position towards the desired target position
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetSwayPosition, Time.deltaTime * swaySmoothSpeed);
    }
}

