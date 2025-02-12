using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{

    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    public InputActionReference secondAction;
    bool grabbing = false;

    private Vector3 previousPosition;
    private Quaternion previousRotation;
    public bool doubleRotationEnabled = false;


    private void Start()
    {
        action.action.Enable();
        if (secondAction != null)
            secondAction.action.Enable();
        previousPosition = transform.position;
        previousRotation = transform.rotation;

        foreach (CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
    }

    void Update()
    {
        if (secondAction != null && secondAction.action.WasPressedThisFrame())
        {
            doubleRotationEnabled = !doubleRotationEnabled;
            Debug.Log("Double Rotation: " + doubleRotationEnabled);
        }
        grabbing = action.action.IsPressed();

        if (grabbing)
        {
            if (!grabbedObject)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

            if (grabbedObject)
            {
                Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);

                //if (doubleRotationEnabled)
                //{
                //    float angle;
                //    Vector3 axis;
                //    deltaRotation.ToAngleAxis(out angle, out axis);
                //    angle *= 2;
                //    deltaRotation = Quaternion.AngleAxis(angle, axis);
                //}
                if (doubleRotationEnabled)
                {
                    deltaRotation *= deltaRotation; 
                }

                Vector3 previousOffset = grabbedObject.position - previousPosition;
                Vector3 rotatedOffset = deltaRotation * previousOffset;
                grabbedObject.position = transform.position + rotatedOffset;

                grabbedObject.rotation = deltaRotation * grabbedObject.rotation;
            }
        }
        else if (grabbedObject)
        {
            grabbedObject = null;
        }

        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
            nearObjects.Remove(t);
    }
}