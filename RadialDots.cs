using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialDots : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Layers { Default, River, Velo }
    public Layers Layer;
    void Start()
    {
        
    }

    public void OnBigCircle()
    {
        if (Layer == Layers.Velo)
        {
            ZeroPointUI.me1.iconsController.ShowVelo();
        }
    }

    public void OnAnimationEnd()
    {
        if (Layer == Layers.River)
        {
            transform.parent.GetComponent<RiverController>().ShowIcons();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
