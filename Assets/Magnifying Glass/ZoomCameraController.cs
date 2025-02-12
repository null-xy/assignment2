using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform lens;
    public Transform vrCamera;
    public Transform lensFrame;
    private bool isBackLens;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = lens.position;
        //transform.rotation = vrCamera.rotation;
        float frameZ = lensFrame.localEulerAngles.z;
        Vector3 screenAngles = lens.localEulerAngles;
        isBackLens = (Mathf.Abs(lens.localEulerAngles.y - 180f) < 1f);
        if (!isBackLens)
        {
            screenAngles.z = -frameZ;
        }
        else
        {
            screenAngles.z = frameZ;
        }
        lens.localEulerAngles = screenAngles;
        //transform.rotation = vrCamera.rotation* (lens.rotation);
        Vector3 forward = (transform.position- vrCamera.position).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
        Vector3 up = Vector3.Cross(forward, right).normalized;
        transform.rotation = Quaternion.LookRotation(forward, up);
    }
}
