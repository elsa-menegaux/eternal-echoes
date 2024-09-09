using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using System;
using System.Linq;

public class OptionsMenu : MonoBehaviour
{
    public class IntRes : IComparable<IntRes>
    {
        public int width;
        public int height;

        public IntRes(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int CompareTo(IntRes other)
        {
            return (width.CompareTo(other.width) + height.CompareTo(other.height)) / 2;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            IntRes resobj = (IntRes) obj;

            return resobj.width == width && resobj.height == height;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return this.width.GetHashCode() * 17 + this.height.GetHashCode();
        }
    }

    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown refreshRateDropdown;
    public TMP_Dropdown qualityDropdown;

    IntRes[] resolutionsArray;
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

        SortedSet<RefreshRate> refreshRatesSet = new SortedSet<RefreshRate>();
        SortedSet<IntRes> resolutionsSet = new SortedSet<IntRes>();

        foreach (Resolution res in providedResolutions)
        {
            refreshRatesSet.Add(res.refreshRateRatio);
            resolutionsSet.Add(new IntRes(res.width, res.height));
        }

        //Set Resolutions Dropdown
        resolutionsArray = resolutionsSet.ToArray<IntRes>();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutionsArray.Length; i++)
        {
            string option = resolutionsArray[i].width + " x " + resolutionsArray[i].height;
            options.Add(option);

            if (resolutionsArray[i].width == Screen.currentResolution.width && resolutionsArray[i].height == Screen.currentResolution.height)
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
        IntRes resolution = resolutionsArray[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, Screen.currentResolution.refreshRateRatio);
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
