using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingIconsLayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Disable()
    {
        foreach (Icon icon in transform.GetComponentsInChildren<Icon>())
        {
            icon.SetStatus(false);
        }
    }

    public void Enable()
    {
        foreach (Icon icon in transform.GetComponentsInChildren<Icon>())
        {
            icon.SetStatus(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
