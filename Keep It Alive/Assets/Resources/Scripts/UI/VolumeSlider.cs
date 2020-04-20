using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string volumeType;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(volumeType + "Vol", Mathf.Log10(sliderValue) * 20);
    }
}
