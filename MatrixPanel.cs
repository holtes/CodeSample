using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class MatrixPanel : MonoBehaviour
{
    public float strToFloat(string str)
    {
        return float.Parse(str, CultureInfo.InvariantCulture.NumberFormat);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        ZeroPointUI.me1.matrixController.gameObject.SetActive(true);
        string jsonSrc = JsonDBProvider.getJsonString("PeopleFlow");
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
