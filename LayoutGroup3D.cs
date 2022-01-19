using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis { X, Y, Z };

public class CustomVector3
{
    public Vector3 vectorData;
    
    public void SetByAxis(Axis axis, float value)
    {
        switch (axis)
        {
            case Axis.X:
                vectorData.x = value;
                break;
            case Axis.Y:
                vectorData.y = value;
                break;
            case Axis.Z:
                vectorData.z = value;
                break;

        }
    }

    public float GetByAxis(Axis axis)
    {
        float data = 0;
        switch (axis)
        {
            case Axis.X:
                data = vectorData.x;
                break;
            case Axis.Y:
                data = vectorData.y;
                break;
            case Axis.Z:
                data = vectorData.z;
                break;
        }
        return data;
    }
}
public class LayoutGroup3D : MonoBehaviour
{
    


    // Start is called before the first frame update
    public int objectsCount;
    public float objectsDistance;
    public Axis gridAxis;
    public string objectPrefix;
    
    public GameObject objectPrefab;
    public Vector3 localScale;

    private void OnValidate()
    {
        for (int i = 0;i<transform.childCount;i++)
        {
            DestroyImmediate(transform.GetChild(i).gameObject, true);
        }
        if (objectsCount == 0 || (objectsDistance == 0 && objectsCount != 1) || objectPrefab == null)
        {
            return;
        }
        var newPosition = new CustomVector3();
        newPosition.vectorData = transform.position;
        newPosition.SetByAxis(gridAxis, newPosition.GetByAxis(gridAxis) - (objectsDistance * objectsCount / 2));
        for (int i = 0;i<objectsCount;i++)
        {
            GameObject newObj = Instantiate(objectPrefab, transform);
            newObj.transform.position = newPosition.vectorData;
            newObj.transform.localScale = localScale;
            newObj.name = objectPrefix + i.ToString();
            newPosition.SetByAxis(gridAxis, newPosition.GetByAxis(gridAxis) + objectsDistance);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
