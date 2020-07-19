using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;


public class SettingsPanel : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider sliderMusic;
    public TextMeshProUGUI textMusicVolume;
    public Slider sliderSFX;
    public TextMeshProUGUI textSFXVolume;

    private float sliderOffset = 80;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        audioMixer.GetFloat("Background", out float valueMusic);
        SetMusicVolumeLevel(valueMusic + sliderOffset);
        sliderMusic.value = valueMusic + sliderOffset;

        audioMixer.GetFloat("SFX", out float valueSFX);
        SetSFXVolumeLevel(valueSFX + sliderOffset);
        sliderSFX.value = valueSFX + sliderOffset;

        sliderMusic.onValueChanged.AddListener(MusicVolumeChange);
        sliderSFX.onValueChanged.AddListener(SFXVolumeChange);
    }


    private void OnDisable()
    {
        sliderMusic.onValueChanged.RemoveListener(MusicVolumeChange);
        sliderSFX.onValueChanged.RemoveListener(SFXVolumeChange);        
    }

    private void MusicVolumeChange(float value)
    {
        audioMixer.SetFloat("Background", value - sliderOffset);
        SetMusicVolumeLevel(value);
    }

    private void SFXVolumeChange(float value)
    {
        audioMixer.SetFloat("SFX", value - sliderOffset);
        SetSFXVolumeLevel(value);
    }

    private void SetVolumeLevel(TextMeshProUGUI tmp, float value)
    {
        tmp.text = value.ToString("F0");
    }

    private void SetMusicVolumeLevel(float value)
    {
        SetVolumeLevel(textMusicVolume, value);
    }

    private void SetSFXVolumeLevel(float value)
    {
        SetVolumeLevel(textSFXVolume, value);
    }

}
