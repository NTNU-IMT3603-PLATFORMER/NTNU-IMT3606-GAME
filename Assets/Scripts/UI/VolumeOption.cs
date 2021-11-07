using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeOption : MonoBehaviour {

    [SerializeField, Tooltip("Text displaying volume percentage")]   TMPro.TextMeshProUGUI _volumePercentageText;

    public void OnVolumeChanged (float volumePercentage) {
        // Set volume text with appropriate amount of decimals
        _volumePercentageText.text = (volumePercentage * 100f).ToString("0.0") + "%";

        // Update global volume
        AudioListener.volume = volumePercentage;
    }

}