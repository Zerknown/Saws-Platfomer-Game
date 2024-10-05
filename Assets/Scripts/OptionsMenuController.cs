using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // Reference to your AudioMixer
    [SerializeField] private Slider musicSlider;    // Reference to your music slider
    [SerializeField] private Slider sfxSlider;      // Reference to your SFX slider

    private GameSettings settings;

    // Start is called before the first frame update
    void Start()
    {
        // Load settings from file
        settings = SettingsManager.LoadSettings();

        // Set slider values from loaded settings
        musicSlider.value = settings.musicVolume;
        sfxSlider.value = settings.sfxVolume;

        // Add listener to respond to slider changes in real time
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        settings.musicVolume = volume;
        SettingsManager.SaveSettings(settings); // Save the updated settings
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        settings.sfxVolume = volume;
        SettingsManager.SaveSettings(settings); // Save the updated settings
    }
}
