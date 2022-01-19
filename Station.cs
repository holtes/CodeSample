using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    // Start is called before the first frame update
    public MeshRenderer[] meshes;
    public List<Material> materials;
    public bool makeOpaque;
    public bool makeRealSize;
    public Color color;
    public Color color_prev;
    public int count;

    public enum SurfaceType
    {
        Opaque,
        Transparent
    }

    public enum BlendMode
    {
        Alpha,
        Premultiply,
        Additive,
        Multiply
    }


    public void Start()
    {
        ////   print(transform.GetComponentsInChildren<MeshRenderer>().Length);
        //Shader shdr = Shader.Find("Universal Render Pipeline/Lit");
        //Material m = new Material(shdr);
        //m.SetFloat("_Surface", (float)SurfaceType.Transparent);
        //m.SetFloat("_Blend", (float)BlendMode.Alpha);
        //m.color = new Vector4(0, 0, 0, 0.4f);
        foreach (MeshRenderer meshRenderer in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (ZeroPointUI.isNumber(meshRenderer.gameObject.name))
            {
                meshRenderer.enabled = false;
            }
            materials.Add(meshRenderer.material);
        }
    }

    public void SetStation(int count, Color color, bool under = false)
    {
        if (materials.Count == 0 || meshes.Length == 0)
        {
            return;
        }
        color_prev = color;
        transform.GetComponentsInChildren<MeshRenderer>()[3].material.color = color;
        //for (int i = 0; i < 3; i++)
        //{
        //    //materials[i].color = color;
        //    if (i < count)
        //    {
        //        meshes[i].enabled = true;
        //    }
        //    else
        //    {
        //        meshes[i].enabled = false;
        //    }
        //}
        //if (under)
        //{
        //    foreach (Material mat in materials)
        //    {
        //        mat.color = color;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (makeOpaque)
        {
            foreach (Material mat in materials)
            {
                if (mat.color.a < 1)
                {
                    mat.color += new Color(0, 0, 0, Time.deltaTime * 0.60f);
                }
            }
            if (materials[3].color.a >= 1)
            {
                makeOpaque = false;
                
            }
        }
        if (makeRealSize)
        {
            transform.localScale += new Vector3(0, Time.deltaTime * 200, 0);
            if (transform.localScale.y >= 385.73f)
            {
                makeRealSize = false;
            }
        }
    }
}
