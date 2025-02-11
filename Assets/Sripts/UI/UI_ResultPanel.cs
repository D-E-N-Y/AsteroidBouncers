using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ResultPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public enum E_Result
    {
        Victory,
        Lose
    }

    public void Show(E_Result result, int score)
    {
        resultText.text = result.ToString();
        
        AudioSystem.current.StopMusic();
        AudioSystem.current.PlaySFX(result.ToString());

        scoreText.text = score.ToString();
    }

    public void PressRestartButton()
    {
        GameSystem.current.SetExit();
        
        AudioSystem.current.PlayMusic("Theme");
        AudioSystem.current.PlaySFX("Click");
        SceneManager.LoadScene("GameScene");
    }

    public void PressQuitButton()
    {
        GameSystem.current.SetExit();
        
        AudioSystem.current.PlayMusic("Theme");
        AudioSystem.current.PlaySFX("Click");
        SceneManager.LoadScene("MainMenuScene");
    }
}
