using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer masterVolumeMixer;
    public AudioMixer soundEffectsMixer;
    public AudioMixer backgroundMixer;

    // Volume Control
    public void SetMasterVolume(float volume)
    {
        masterVolumeMixer.SetFloat("Volume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        soundEffectsMixer.SetFloat("SFX", volume);
    }

    public void SetBGMVolume(float volume)
    {
        backgroundMixer.SetFloat("BGM", volume);
    }
}
