using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObjectGizmo : MonoBehaviour
{
    // Start is called before the first frame update
    public Color outlineColor = Color.red;
    private GameObject outlineObject;
    void Start()
    {
        CreateOutline();
    }
    void CreateOutline()
    {
        outlineObject = new GameObject("Outline");
        outlineObject.transform.SetParent(transform);
        outlineObject.transform.localPosition = Vector3.zero;
        outlineObject.transform.localRotation = Quaternion.identity;
        outlineObject.transform.localScale = Vector3.one;

        LineRenderer lineRenderer = outlineObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = outlineColor;
        lineRenderer.endColor = outlineColor;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.loop = true;

        Vector3[] corners = new Vector3[5];
        Bounds bounds = GetComponent<Renderer>().bounds;
        corners[0] = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
        corners[1] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        corners[2] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        corners[3] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        corners[4] = corners[0];

        lineRenderer.positionCount = corners.Length;
        lineRenderer.SetPositions(corners);

        outlineObject.layer = 0;
    }
    void OnDestroy()
    {
        if (outlineObject != null)
        {
            Destroy(outlineObject);
        }
    }
}
