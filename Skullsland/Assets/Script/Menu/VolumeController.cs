using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider MusicvolumeSlider = null;
    [SerializeField] private AudioMixer MusicMixer;
    [SerializeField] private Slider SoundEffectvolumeSlider = null;
    [SerializeField] private AudioMixer SoundEffectMixer;

    [SerializeField] new AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        MusicLoadValues();
        SoundEffectLoadValues();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            audio.Play();
        }

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
