using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        //For drawing line in the world space, provide the x,y,z values
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0)); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, new Vector3(1000, 1000, 1000));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
