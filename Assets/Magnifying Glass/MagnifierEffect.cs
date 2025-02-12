using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifierEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera vrCamera;
    public Camera magnifierCamera;
    public RenderTexture magnifierTexture;
    public Transform magnifierFrame;
    public float magnification = 5.0f;
    [Header("Dual Side Settings")]
    public bool isFlipped = false;
    private Vector3 flipRotation = new Vector3(0, 180, 0);
    private Quaternion initialFrameRotation;
    private Quaternion flipCompensation;
    void Start()
    {
        if (magnifierCamera != null && magnifierFrame!=null)
            magnifierCamera.targetTexture = magnifierTexture;
            magnifierCamera.enabled = true;
            initialFrameRotation = magnifierFrame.rotation;
        flipCompensation = Quaternion.Euler(flipRotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (vrCamera != null && magnifierCamera != null)
        {
            //Matrix4x4 currentFrameMatrix = magnifierFrame.localToWorldMatrix;
            Quaternion frameDeltaRotation = magnifierFrame.rotation * Quaternion.Inverse(initialFrameRotation);
            Quaternion baseRotation = isFlipped ? flipCompensation * vrCamera.transform.rotation : vrCamera.transform.rotation;
            Quaternion finalRotation = Quaternion.Inverse(frameDeltaRotation) * baseRotation;
            magnifierCamera.transform.rotation = finalRotation;
            //magnifierCamera.transform.position = magnifierFrame.transform.position;
            //Quaternion vrRotation = vrCamera.transform.rotation;
            //Quaternion magnifierRotation = magnifierFrame.transform.rotation;
            //magnifierCamera.transform.rotation = magnifierRotation * vrRotation;
            //magnifierCamera.transform.rotation = Quaternion.Inverse(magnifierRotation) * vrRotation;

            magnifierCamera.fieldOfView = vrCamera.fieldOfView / magnification;
        }
    }
}
