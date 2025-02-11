using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_SettingsPanel : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    
    [Serializable]
    private struct OnOffImage
    {
        public Image onImage;
        public Image offImage;

        public OnOffImage(Image onImage, Image offImage)
        {
            this.onImage = onImage;
            this.offImage = offImage;
        }
    }
    [SerializeField] private OnOffImage musicImages;
    [SerializeField] private OnOffImage soundImages;

    void Start()
    {
        musicImages.onImage.gameObject.SetActive(!AudioSystem.current.isMuteMusic());
        musicImages.offImage.gameObject.SetActive(AudioSystem.current.isMuteMusic());

        soundImages.onImage.gameObject.SetActive(!AudioSystem.current.isMuteSFX());
        soundImages.offImage.gameObject.SetActive(AudioSystem.current.isMuteSFX());

        musicVolumeSlider.value = AudioSystem.current.GetVolumeMusic();
        sfxVolumeSlider.value = AudioSystem.current.GetVolumeSFX();
    }

    public void PressMusicButton()
    {
        AudioSystem.current.PlaySFX("Click");
        
        AudioSystem.current.ToggleMusic();
        musicImages.onImage.gameObject.SetActive(!AudioSystem.current.isMuteMusic());
        musicImages.offImage.gameObject.SetActive(AudioSystem.current.isMuteMusic());
    }

    public void PressSoundButton()
    {
        AudioSystem.current.PlaySFX("Click");
        
        AudioSystem.current.ToggleSFX();
        soundImages.onImage.gameObject.SetActive(!AudioSystem.current.isMuteSFX());
        soundImages.offImage.gameObject.SetActive(AudioSystem.current.isMuteSFX());
    }

    public void PressContinueButton()
    {
        AudioSystem.current.PlaySFX("Click");
        gameObject.SetActive(false);
    }

    public void PressQuitButton()
    {
        AudioSystem.current.PlaySFX("Click");
        SceneManager.LoadScene("MainMenuScene");
    }

    public void MusicVolume()
    {
        AudioSystem.current.MusicVolume(musicVolumeSlider.value);
    }

    public void SFXVolume()
    {
        AudioSystem.current.SFXVolume(sfxVolumeSlider.value);
    }
}
