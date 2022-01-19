using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour {

    public GameObject slider;
    public GameObject toggles;


    public void SwitchMenu(bool state)
    {
        slider.SetActive(state);
        toggles.SetActive(!state);
    }
}
