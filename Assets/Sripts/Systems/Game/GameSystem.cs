using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem current;
    public Action<string> UpdateNamePlanet;
    public Action<int> UpdateScore;

    [SerializeField] private UI_TopPanel ui_TopPanel;
    [SerializeField] private UI_BottonPanel ui_BottonPanel;

    [SerializeField] private List<Planet> planets;
    private int currentPlanet;
    
    [SerializeField] private Cannon cannon;
    private int score;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        score = 0;
        currentPlanet = 0;
        
        ui_TopPanel.Ininitialize();
        // ui_BottonPanel.Ininitialize();

        planets[currentPlanet].gameObject.SetActive(true);
        planets[currentPlanet].Initialize();

        UpdateNamePlanet?.Invoke(planets[currentPlanet].GetName());
        UpdateScore?.Invoke(score);

        cannon.gameObject.SetActive(true);
        cannon.Initialize(planets[currentPlanet].GetColors());
    }

    public void NextPlanet()
    {
        planets[currentPlanet].gameObject.SetActive(false);
        
        currentPlanet++;
        planets[currentPlanet].gameObject.SetActive(true);
        planets[currentPlanet].Initialize();

        UpdateNamePlanet?.Invoke(planets[currentPlanet].GetName());

        cannon.Initialize(planets[currentPlanet].GetColors());
    }

    public void AddScore()
    {
        score++;
        UpdateScore?.Invoke(score);
    }
}
