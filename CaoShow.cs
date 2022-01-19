using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaoShow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnStartShowPodlojka()
    {
        transform.parent.GetComponent<MatrixController>().ShowPodlojka();
    }

    public void OnAnimationEnd()
    {
        GetComponent<Animator>().Rebind();
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = null;
        ZeroPointUI.me1.matrixController.SetOpacityToDistrict("CAO");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
