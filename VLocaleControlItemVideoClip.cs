using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Vizart{
[System.Serializable]
public class VLocaleControlItemVideoClip 
{
    public VideoPlayer CtlVidePlayer; // контролируемый объект
//    public VLocale.langs lang=VLocale.langs.none;
    [HideInInspector]
    public string key;
    [HideInInspector]
    public bool isInit=false;
    public string getKey(){
        if(isInit) return key;
        return CtlVidePlayer.clip.name;
    }
    public void loadClip(string clipName){
        bool isPlay=CtlVidePlayer.isPlaying;
        if(isPlay) CtlVidePlayer.Stop();
        VideoClip curClip =Resources.Load<VideoClip>("Video/"+clipName);
        CtlVidePlayer.clip=curClip;
        if(isPlay) CtlVidePlayer.Play();
    }
}
}
