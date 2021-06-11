using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEditor;

public class VolumeController : MonoBehaviour
{
    public Slider MusicvolumeSlider = null;
    public AudioMixer MusicMixer;
    public Slider SoundEffectvolumeSlider = null;
    public AudioMixer SoundEffectMixer;

 
    // Start is called before the first frame update
    void Start()
    {
        MusicLoadValues();

        SoundEffectLoadValues();
    }


    public void MusicSaveVolumeButton()
    {
        float volumeValue = MusicvolumeSlider.value;

        PlayerPrefs.SetFloat("Music",volumeValue);

        MusicMixer.SetFloat("Music", Mathf.Log10(volumeValue) * 20);

        MusicLoadValues();
    }
    void MusicLoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("Music");
        
        MusicvolumeSlider.value = volumeValue; 
    }

    public void SoundEffectSaveVolumeButton()
    {
        float volumeValue = SoundEffectvolumeSlider.value;

        PlayerPrefs.SetFloat("SoundEffects", volumeValue);

        SoundEffectMixer.SetFloat("SoundEffects", Mathf.Log10(volumeValue) * 20);

        SoundEffectLoadValues();
    }
    void SoundEffectLoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("SoundEffects");
        
        SoundEffectvolumeSlider.value = volumeValue;
    }
}
