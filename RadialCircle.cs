using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowAllMatrixIcons()
    {
        transform.parent.GetComponent<MatrixController>().ShowAllMatrixIcons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
