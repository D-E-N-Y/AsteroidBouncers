using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    
    public void PressPlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void PressSettingsButton()
    {
        settingPanel.SetActive(true);
    }

    public void PressExitButton()
    {
        Application.Quit();
    }
}
