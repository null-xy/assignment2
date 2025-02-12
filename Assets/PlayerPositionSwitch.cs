using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerPositionSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionReference switchButton;
    public InputActionReference moveAction;
    public float moveSpeed = 3.0f;

    private Vector3 roomPosition = new Vector3(0, 0, 0);
    private Vector3 externalPosition = new Vector3(0, 0, -30);
    private bool isInRoom = true;

    void Start()
    {
        transform.position = roomPosition;
        switchButton.action.Enable();
        moveAction.action.Enable();
    }
    void OnEnable()
    {
        if (switchButton != null)
            switchButton.action.Enable();
        if (moveAction != null) 
            moveAction.action.Enable();
    }
    void OnDisable()
    {
        if (switchButton != null)
            switchButton.action.Disable();
        if (moveAction != null)
            moveAction.action.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        CheckPlayerPosition();
        if (switchButton.action.WasPressedThisFrame())
        {
            SwitchPosition();
        }
    }
    private void HandleMovement()
    {
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    private void CheckPlayerPosition()
    {
        float playerX = transform.position.x;
        float playerZ = transform.position.z;

        if (playerX >= -7.5f && playerX <= 7.5f  && playerZ >= -7.5f && playerZ <= 7.5f)
        {
            isInRoom = true;
        }
        else
        {
            isInRoom = false;
        }
    }
    private void SwitchPosition()
    {
        if (isInRoom)
        {
            transform.position = externalPosition;
        }
        else
        {
            transform.position = roomPosition;
        }
        isInRoom = !isInRoom;
    }

}
