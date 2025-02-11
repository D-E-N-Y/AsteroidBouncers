using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_SettingsPanel : MonoBehaviour
{
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
    private bool isMusic = true;
    
    [SerializeField] private OnOffImage soundImages;
    private bool isSound = true;

    public void PressMusicButton()
    {
        isMusic = !isMusic;

        musicImages.onImage.gameObject.SetActive(isMusic);
        musicImages.offImage.gameObject.SetActive(!isMusic);
    }

    public void PressSoundButton()
    {
        isSound = !isSound;

        soundImages.onImage.gameObject.SetActive(isSound);
        soundImages.offImage.gameObject.SetActive(!isSound);
    }

    public void PressContinueButton()
    {
        gameObject.SetActive(false);
    }

    public void PressQuitButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
