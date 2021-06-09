using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfigurationOptions: MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    public GameObject PrefabBloom;
    public static bool Bloom = true;
    
    Resolution[] resolutions;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        //Resolução

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++) 
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);

        //Resolução


        PrefabBloom.SetActive(Bloom);

    }


    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullScreen(bool isfull)
    {
        Screen.fullScreen = isfull;
    }
    public void SetBloom(bool isbloom)
    {
        Bloom = isbloom;
        PrefabBloom.SetActive(isbloom);
    }
}
