using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1.0f);
        }

        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        AudioListener.volume = volumeSlider.value;
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        AudioListener.volume = volumeSlider.value;
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
