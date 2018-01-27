using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent(typeof(LineRenderer))]
public class LineRendererTest : MonoBehaviour
{
    List<Vector3> linePoints = new List<Vector3>();
    LineRenderer lineRenderer;
    public float threshold = 0.001f;
    Camera thisCamera;
    int lineCount = 0;

    Vector3 lastPos = Vector3.one * float.MaxValue;
    AnimationCurve curve = new AnimationCurve();


    void Awake()
    {
        thisCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        curve.AddKey(0, 0.2f);
        curve.AddKey(1, 0.2f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = thisCamera.nearClipPlane;
            Vector3 mouseWorld = thisCamera.ScreenToWorldPoint(mousePos);

            float dist = Vector3.Distance(lastPos, mouseWorld);
            if (dist <= threshold)
                return;

            lastPos = mouseWorld;
            if (linePoints == null)
                linePoints = new List<Vector3>();
            linePoints.Add(mouseWorld);

            UpdateLine();
        }
    }


    void UpdateLine()
    {
        lineRenderer.widthCurve = curve;

        lineRenderer.positionCount = linePoints.Count;

        for (int i = lineCount; i < linePoints.Count; i++)
        {
            lineRenderer.SetPosition(i, linePoints[i]);
        }
        lineCount = linePoints.Count;
    }
}