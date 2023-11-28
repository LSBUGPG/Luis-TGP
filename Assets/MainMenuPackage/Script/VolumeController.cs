using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMasterLevel(float sliderValue)
    { 
        mixer.SetFloat ("Master Vol", Mathf.Log10(sliderValue) *20);
    
    }
    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("Music Vol", Mathf.Log10(sliderValue) * 20);

    }
    public void SetSoundLevel(float sliderValue)
    {
        mixer.SetFloat("Sound Vol", Mathf.Log10(sliderValue) * 20);

    }

}
