using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeOption : MonoBehaviour, IOption {

    [SerializeField, Tooltip("Text displaying volume percentage")]      TMPro.TextMeshProUGUI _volumePercentageText;
    [SerializeField, Tooltip("Slider for volume percentage")]           Slider _volumeSlider;

    public void InitializeOption () {
        // Load previous volume settings
        float volumePercentage = PlayerPrefs.GetFloat("Volume", 1f);

        UpdateText(volumePercentage);
        _volumeSlider.value = volumePercentage;
        AudioListener.volume = volumePercentage;
    }

    public void OnVolumeChanged (float volumePercentage) {
        UpdateText(volumePercentage);

        // Update global volume
        AudioListener.volume = volumePercentage;

        // Save volume settings
        PlayerPrefs.SetFloat("Volume", volumePercentage);
        PlayerPrefs.Save();
    }

    void UpdateText (float volumePercentage) {
        // Set volume text with appropriate amount of decimals
        _volumePercentageText.text = (volumePercentage * 100f).ToString("0.0") + "%";
    }

}