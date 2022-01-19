using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixController : MonoBehaviour
{
    // Start is called before the first frame update
    public float lineSpawnTimeout;
    public GameObject linePrefab;
    public Material opacityMaterial;
    public PF_Structure pfData;
    void Start()
    {

    }

    public void Disable()
    {
        linePrefab.GetComponent<Line>().speed = 1500;
        lineSpawnTimeout = 0.25f;
        foreach (MatrixIconController mic in transform.GetComponentsInChildren<MatrixIconController>())
        {
            mic.iconAnimator.Rebind();
            mic.iconAnimator.enabled = false;
            mic.dotCentralAnimator.Rebind();
            mic.standbyAnimator.Rebind();

        }
        transform.Find("CAO").GetComponent<MeshRenderer>().material = opacityMaterial;
        transform.gameObject.SetActive(false);
    }

    public void SetOpacityToDistrict(string district)
    {
        Material activeMaterial = Resources.Load<Material>("HighlightDistricts/" + district);
        transform.Find(district).GetComponent<MeshRenderer>().material = activeMaterial;
    }

    public void ShowAllMatrixIcons()
    {
        StartCoroutine(StartSlowLine());
        MatrixIconController[] mics = transform.GetComponentsInChildren<MatrixIconController>();
        for (int i = 0;i < mics.Length - 1;i++)
        {
            StartCoroutine(ShowIcon(mics[i], (float)i / 10));
        }
        mics[8].transform.GetChild(0).gameObject.SetActive(true);
        mics[8].transform.GetChild(1).gameObject.SetActive(false);
        //StartCoroutine(SpawnAllLines("CAO", (float)(mics.Length - 2) / 10));
    }

    public IEnumerator StartSlowLine()
    {
        yield return new WaitForSeconds(3);
        linePrefab.GetComponent<Line>().speed = 200;
        lineSpawnTimeout = 1.3f;
        foreach (MatrixIconController mic in transform.GetComponentsInChildren<MatrixIconController>())
        {
            foreach (Line ln in mic.transform.parent.GetComponentsInChildren<Line>())
            {
                ln.speed = 200;
            }
        }
    }


    public IEnumerator ShowIcon(MatrixIconController mic, float timeout)
    {
        yield return new WaitForSeconds(timeout / 2);
        SpawnOneLine(mic);
        yield return new WaitForSeconds(timeout / 2);
        mic.iconAnimator.enabled = true;
    }

    public void ShowCao()
    {
        SetDistrict("CAO", false, false);
        transform.Find("CaoAnimation").GetComponent<Animator>().enabled = true;
        linePrefab.GetComponent<Line>().speed = 1000;
    }

    public void ShowPodlojka()
    {
        transform.Find("RadialCircle").GetComponent<Animator>().enabled = true;
    }

    public void ClearLines()
    {
        foreach (Line ln in transform.GetComponentsInChildren<Line>())
        {
            Destroy(ln.gameObject);
        }
    }

    public void SpawnOneLine(MatrixIconController mic_two)
    {
        transform.Find("CAO").GetComponentInChildren<MatrixIconController>().StartSpawnLine(lineSpawnTimeout, Color.yellow, mic_two.transform.position, "CAO");
        mic_two.StartSpawnLine(lineSpawnTimeout, Color.white, transform.Find("CAO").GetChild(0).transform.position, "CAO");
        //if (dstr.name == district)
        //{
        //    foreach (MatrixIconController mic_second in transform.GetComponentsInChildren<MatrixIconController>())
        //    {
        //        if (mic == mic_second)
        //        {
        //            continue;
        //        }
                
        //    }
        //}
        //else
        //{
            
        //}
    }

    public IEnumerator SpawnAllLines(string district, float timeout = 0)
    {
        yield return new WaitForSeconds(timeout);
        ClearLines();
        foreach (MatrixIconController mic in transform.GetComponentsInChildren<MatrixIconController>())
        {
            GameObject dstr = mic.transform.parent.gameObject;;
            if (dstr.name == district)
            {
                foreach (MatrixIconController mic_second in transform.GetComponentsInChildren<MatrixIconController>())
                {
                    if (mic == mic_second)
                    {
                        continue;
                    }
                    mic.StartSpawnLine(lineSpawnTimeout, Color.yellow, mic_second.transform.position, district);
                }
            }
            else
            {
                mic.StartSpawnLine(lineSpawnTimeout, Color.white, transform.Find(district).GetChild(0).transform.position, district);
            }

        }
        yield return null;
    }

    public void SetDistrict(string district, bool showMaterials = true, bool spawnLines = true)
    {
        if (pfData == null || pfData.MatrixData==null) return;
        foreach (PF_District dstr in pfData.MatrixData)
        {
            if (dstr.name == district)
            {
                foreach (PF_DistrictData data in dstr.from)
                {
                    if (data.name == district)
                    {
                        continue;
                    }
                    Transform trns = transform.Find(data.name);
                    if (trns == null)
                    {
                        continue;
                    }
                    MatrixIconController matrix = trns.GetComponentInChildren<MatrixIconController>();
                    matrix.SetStatus(true);
                    matrix.SetText(1, data.value);
                }
                foreach (PF_DistrictData data in dstr.to)
                {
                    if (data.name == district)
                    {
                        continue;
                    }
                    Transform trns = transform.Find(data.name);
                    if (trns == null)
                    {
                        continue;
                    }
                    MatrixIconController matrix = trns.GetComponentInChildren<MatrixIconController>();
                    matrix.SetText(0, data.value);
                }

                break;
            }
        }
        if (showMaterials)
        {
            foreach (MatrixIconController mic in transform.GetComponentsInChildren<MatrixIconController>())
            {
                GameObject dstr = mic.transform.parent.gameObject;
                if (dstr.name == district)
                {
                    mic.SetStatus(false);
                    Material activeMaterial = Resources.Load<Material>("HighlightDistricts/" + district);
                    dstr.GetComponent<MeshRenderer>().material = activeMaterial;
                    dstr.transform.Find("Mask").gameObject.SetActive(true);
                }
                else
                {
                    //Material passMaterial = Resources.Load<Material>("Pixelate/" + dstr.name);
                    dstr.GetComponent<MeshRenderer>().material = opacityMaterial;
                    dstr.transform.Find("Mask").gameObject.SetActive(false);
                    if (mic.iconAnimator.GetComponent<SpriteRenderer>().sprite.name.Contains("0000"))
                    {
                        mic.iconAnimator.enabled = true;
                    }
                }

            }
        }
        if (spawnLines)
        {
            ClearLines();
            StartCoroutine(SpawnAllLines(district));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
