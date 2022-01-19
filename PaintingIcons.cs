using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingIcons : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeout;
    public GameObject iconPrefab;
    private bool stop;
    void Start()
    {
        
    }

    public void StartPaint()
    {
        stop = false;
        StartCoroutine(Paint());
    }

    public void StopPaint()
    {
        stop = true;
    }

    IEnumerator Paint()
    {
        GameObject newObj = Instantiate(iconPrefab);
        newObj.transform.SetParent(transform.parent);
        newObj.transform.localPosition = transform.localPosition;
        newObj.transform.localScale = new Vector3(0.387543321f, 0.387543321f, 0.387543321f);
        yield return new WaitForSeconds(timeout);
        if (!stop)
        {
            yield return Paint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
