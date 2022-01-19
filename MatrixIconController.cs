using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatrixIconController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dotCentral;
    public Animator iconAnimator;
    public Animator dotCentralAnimator;
    public Animator standbyAnimator;
    public TMP_Text[] textsToSet;

    public void SetText(int number, string text)
    {
        textsToSet[number].text = text;
    }

    void Start()
    {
        
    }

    public void StartSpawnLine(float timeout, Color color, Vector3 target, string okrug)
    {
        if (!gameObject.active)
        {
            return;
        }
        
        StartCoroutine(SpawnLine(timeout, color, target, okrug));
    }

    public void SetStatus(bool status)
    {
        transform.GetChild(0).gameObject.SetActive(!status);
        transform.GetChild(1).gameObject.SetActive(status);
    }

    IEnumerator SpawnLine(float timeout, Color color, Vector3 target, string okrug)
    {
        if (okrug == ZeroPointUI.me1.cameraController.selectedOkrug.ToUpper() && gameObject.active)
        {
            GameObject newLine = Instantiate(ZeroPointUI.me1.matrixController.linePrefab);
            Line lnComponent = newLine.GetComponent<Line>();
            lnComponent.transform.position = transform.position;
            newLine.transform.SetParent(transform.parent);
            newLine.GetComponentsInChildren<SpriteRenderer>()[0].color = color;
            newLine.GetComponentsInChildren<SpriteRenderer>()[1].color = color;
            newLine.transform.GetChild(0).gameObject.SetActive(false);
            lnComponent.endPosition = target;
            newLine.transform.LookAt(target);
            lnComponent.moving = true;
            yield return new WaitForSeconds(ZeroPointUI.me1.matrixController.lineSpawnTimeout);
            yield return SpawnLine(timeout, color, target, okrug);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
