using System.Collections;
using UnityEngine;

public class MapController : MonoBehaviour {

    public static MapController I;

    public TreeController trees;
    public GameObject grass;
    public float grass_ShowLevel;
    public GameObject buildings2D;
    public float buildings2D_ShowLevel;
    public GameObject buildings3D;
    public float buildings3D_ShowLevel;

    private bool buildings2DIsShow;
    private bool buildings3DIsShow;

    void Awake(){
        I = this;
    }

    void Start()
    {
        CameraController.OnZoomChanged += OnZoomChanged;
    }

    void OnZoomChanged(int zoomLevel,float zoomVal)
    {
        if (buildings2D_ShowLevel < zoomVal && !buildings2DIsShow && !buildings3DIsShow)
        {
            buildings2DIsShow = true;
            buildings2D.SetActive(true);
        }

        if (buildings2D_ShowLevel > zoomVal && buildings2DIsShow)
        {
            buildings2DIsShow = false;
            buildings2D.SetActive(false);
        }

        if (buildings3D_ShowLevel < zoomVal && !buildings3DIsShow)
        {
            StopAllCoroutines();
            StartCoroutine(ShowBuilings());   
        }

        if (buildings3D_ShowLevel > zoomVal && buildings3DIsShow)
        {
            StopAllCoroutines();
            StartCoroutine(HideBuilings());            
        }
    }

    public void SetMapLevel(int level)
    {

    }

    IEnumerator ShowBuilings()
    {
        Vector3 scale = new Vector3(1, 0, 1);

        buildings3D.transform.localScale = scale;
        buildings3D.SetActive(true);
        trees.Show();

        buildings3DIsShow = true;

        float c = 0;
        while (c <= 1)
        {
            c += 0.1f;
            scale.y = c;
            buildings3D.transform.localScale = scale;
            yield return null;
        }
        buildings2D.SetActive(false);
    }

    IEnumerator HideBuilings()
    {
        trees.Hide();
        buildings2D.SetActive(true);
        Vector3 scale = Vector3.one;
        float c = 0;
        while (c <= 1)
        {
            c += 0.1f;
            scale.y = 1 - c;
            buildings3D.transform.localScale = scale;
            yield return null;
        }
        buildings3D.SetActive(false);
        buildings3DIsShow = false;        
    }
}
