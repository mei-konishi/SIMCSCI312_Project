using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer masterVolumeMixer;
    public AudioMixer soundEffectsMixer;
    public AudioMixer backgroundMixer;

    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider bgmVolumeSlider;

    void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        bgmVolumeSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        SetMasterVolume(masterVolumeSlider.value);
        SetSFXVolume(sfxVolumeSlider.value);
        SetBGMVolume(bgmVolumeSlider.value);
    }

    // Volume Control
    public void SetMasterVolume(float volume)
    {
        masterVolumeMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("masterVolume", (int)masterVolumeSlider.value);
    }

    public void SetSFXVolume(float volume)
    {
        soundEffectsMixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("sfxVolume", (int)sfxVolumeSlider.value);
    }

    public void SetBGMVolume(float volume)
    {
        backgroundMixer.SetFloat("BGM", volume);
        PlayerPrefs.SetFloat("bgmVolume", (int)bgmVolumeSlider.value);
    }
}
