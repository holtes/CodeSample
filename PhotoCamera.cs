using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PhotoCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public RenderTexture outTexture;
    void Start()
    {
        
    }

    public void Photo()
    {
        var width = outTexture.width;
        var height = outTexture.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        RenderTexture.active = outTexture;
        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        var bytes = tex.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
