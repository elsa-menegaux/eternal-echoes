using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using System;
using System.Linq;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown refreshRateDropdown;
    public TMP_Dropdown qualityDropdown;

    int[][] resolutionsArray;
    RefreshRate[] refreshRatesArray;

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat ("MasterVolume", Mathf.Log10(volume)*20);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    
    void Start()
    {
        //Clear Dropdown UI elements
        resolutionDropdown.ClearOptions();
        refreshRateDropdown.ClearOptions();

        //Gather resolutions and refresh-rates information and separate them out
        Resolution[] providedResolutions = Screen.resolutions;

        HashSet<RefreshRate> refreshRatesSet = new HashSet<RefreshRate>();
        HashSet<int[]> resolutionsSet = new HashSet<int[]>();

        foreach (Resolution res in providedResolutions)
        {
            refreshRatesSet.Add(res.refreshRateRatio);
            resolutionsSet.Add(new int[]{res.width, res.height});
        }

        //Set Resolutions Dropdown
        resolutionsArray = resolutionsSet.ToArray<int[]>();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutionsArray.Length; i++)
        {
            string option = resolutionsArray[i][0] + " x " + resolutionsArray[i][1];
            options.Add(option);

            if (resolutionsArray[i][0] == Screen.currentResolution.width && resolutionsArray[i][1] == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //Set RefreshRates Dropdown
        refreshRatesArray = refreshRatesSet.ToArray<RefreshRate>();
        
        List<string> options2 = new List<string>();
        int currentRefreshRateIndex = 0;
        for (int i = 0; i < refreshRatesArray.Length; i++)
        {
            string option = refreshRatesArray[i].ToString();
            options2.Add(option);

            if (refreshRatesArray[i].Equals(Screen.currentResolution.refreshRateRatio))
            {
                currentRefreshRateIndex = i;
            }
        }
        
        refreshRateDropdown.AddOptions(options2);
        refreshRateDropdown.value = currentRefreshRateIndex;
        refreshRateDropdown.RefreshShownValue();

        //Fill Quality Settings
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList<string>());
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void SetResolution(int resolutionIndex)
    {
        int[] resolution = resolutionsArray[resolutionIndex];
        Screen.SetResolution(resolution[0], resolution[1], Screen.fullScreenMode, Screen.currentResolution.refreshRateRatio);
    }

    public void SetRefreshRate(int refreshRateIndex)
    {
        RefreshRate refreshRate = refreshRatesArray[refreshRateIndex];
        Screen.SetResolution(Screen.width, Screen.height, Screen.fullScreenMode, refreshRate);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
