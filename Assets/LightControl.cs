using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Input;

public class LightControl : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionReference leftHandJoystick;
    public Light cusLight;
    public float colorChangeSpeed = 2.0f;

    private float hue = 0f;
    void Start()
    {
        
    }
    void OnEnable()
    {
        if (leftHandJoystick != null)
            leftHandJoystick.action.Enable();
    }
    void OnDisable()
    {
        if (leftHandJoystick != null)
            leftHandJoystick.action.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 joystickInput = leftHandJoystick.action.ReadValue<Vector2>();
        hue += joystickInput.x * colorChangeSpeed * Time.deltaTime;
        hue = Mathf.Repeat(hue, 1f);
        Color newColor = Color.HSVToRGB(hue, 1f, 1f);
        cusLight.color = newColor;
    }
}
