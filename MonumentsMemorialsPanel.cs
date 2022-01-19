using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentsMemorialsPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite museum;
    public Sprite monument;
    void Start()
    {
        
    }

    private void Awake()
    {
        ZeroPointUI.me1.iconsController.ClearLayer("MM");
        string jsonString = JsonDBProvider.getJsonString("monuments_memorials");
        MM_Structure mmData = JsonUtility.FromJson<MM_Structure>(jsonString);
        //foreach (Monument_Memorial monument_Memorial in mmData.monuments_memorials)
        //{
        //    ZeroPointUI.me1.iconsController.CreateMonument(monument_Memorial.coordinates[1], monument_Memorial.coordinates[0], monument_Memorial.name, monument);
        //}
        foreach (Monument_Memorial monument_Memorial in mmData.museums)
        {
            ZeroPointUI.me1.iconsController.CreateMonument(monument_Memorial.coordinates[1], monument_Memorial.coordinates[0], monument_Memorial.name, museum);
        }

    }

    public void OnEnable()
    {
        ZeroPointUI.me1.iconsController.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        ZeroPointUI.me1.iconsController.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
