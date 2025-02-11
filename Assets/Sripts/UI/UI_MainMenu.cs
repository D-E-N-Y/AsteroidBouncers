using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    
    public void PressPlayButton()
    {
        AudioSystem.current.PlaySFX("Click");
        SceneManager.LoadScene("GameScene");
    }

    public void PressSettingsButton()
    {
        AudioSystem.current.PlaySFX("Click");
        settingPanel.SetActive(true);
    }

    public void PressExitButton()
    {
        AudioSystem.current.PlaySFX("Click");
        Application.Quit();
    }
}
