using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    
    public void SetMainVolume(float sliderValue)
    {
        masterMixer.SetFloat("SoundFade", Mathf.Log10(sliderValue) * 20);
    }
    
    public void SetMusicVolume(float sliderValue)
    {
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
    
    public void SetSfxVolume(float sliderValue)
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }
}
