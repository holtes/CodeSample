using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficFlowPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnEnable()
    {
        ZeroPointUI.me1.matrixController.gameObject.SetActive(true);
        string jsonSrc = JsonDBProvider.getJsonString("TrafficFlow");
        ZeroPointUI.me1.matrixController.pfData = JsonUtility.FromJson<PF_Structure>(jsonSrc);
        ZeroPointUI.me1.matrixController.ShowCao();
    }

    private void OnDisable()
    {
        ZeroPointUI.me1.matrixController.ClearLines();
        ZeroPointUI.me1.matrixController.Disable();

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
