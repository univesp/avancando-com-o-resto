using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider sliderBGM;
    [SerializeField] private Slider sliderSFX;

    private void OnEnable()
    {
        sliderBGM.value = PlayerPrefs.GetFloat("BGM", sliderBGM.value);
        sliderSFX.value = PlayerPrefs.GetFloat("SFX", sliderSFX.value);
    }

    public void BGMVolume()
    {
        audioMixer.SetFloat("BGM", sliderBGM.value);
        PlayerPrefs.SetFloat("BGMVolume", sliderBGM.value);
    }

    public void SFXVolume()
    {
        audioMixer.SetFloat("SFX", sliderSFX.value);
        PlayerPrefs.SetFloat("SFXVolume", sliderSFX.value);
    }
}
