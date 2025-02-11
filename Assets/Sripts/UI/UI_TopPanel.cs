using TMPro;
using UnityEngine;

public class UI_TopPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI namePlanetText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject settingsPanel;

    public void Initialize()
    {
        GameSystem.current.UpdateNamePlanet += RefreshNamePlanetText;
        GameSystem.current.UpdateScore += RefreshScoreText;
    }

    private void RefreshNamePlanetText(string namePlanet)
    {
        namePlanetText.text = namePlanet;
    }

    private void RefreshScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public void PressSettingsButton()
    {
        settingsPanel.SetActive(true);
    }
}
