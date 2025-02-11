using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem current;
    public Action<string> UpdateNamePlanet;
    public Action<int> UpdateScore;
    public Action<int> UpdateCountProjectiles;

    [SerializeField] private UI_TopPanel ui_TopPanel;
    [SerializeField] private UI_BottonPanel ui_BottonPanel;

    [SerializeField] private List<Planet> planets;
    private int currentPlanet;
    
    [SerializeField] private Cannon cannon;
    private int score;

    [SerializeField] private GameObject resultPanel;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        score = 0;
        currentPlanet = 0;

        ui_TopPanel.Initialize();
        ui_BottonPanel.Initialize();

        planets[currentPlanet].gameObject.SetActive(true);
        planets[currentPlanet].Initialize();

        UpdateNamePlanet?.Invoke(planets[currentPlanet].GetName());
        UpdateScore?.Invoke(score);

        cannon.gameObject.SetActive(true);
        cannon.Initialize(planets[currentPlanet].GetColors(), CalculateCountProjectiles());
    }

    private int CalculateCountProjectiles()
    {
        return Mathf.RoundToInt(Mathf.Pow(planets[currentPlanet].GetRadius() + planets[currentPlanet].GetLayers(), 2));
    }

    private bool isExit = false;
    public void SetExit() => isExit = true;

    public void NextPlanet()
    {
        if(isExit) return;
        
        planets[currentPlanet].Restart();
        planets[currentPlanet].gameObject.SetActive(false);
        
        currentPlanet++;

        if(currentPlanet >= planets.Count)
        {
            // cannon.RemoveProjectile();
            
            resultPanel.SetActive(true);
            resultPanel.GetComponent<UI_ResultPanel>().Show(UI_ResultPanel.E_Result.Victory, score);
            return;
        }

        planets[currentPlanet].gameObject.SetActive(true);
        planets[currentPlanet].Initialize();

        UpdateNamePlanet?.Invoke(planets[currentPlanet].GetName());

        cannon.Initialize(planets[currentPlanet].GetColors(), CalculateCountProjectiles());
    }

    public void CheckCountProjectiles(int countProjectiles)
    {
        UpdateCountProjectiles?.Invoke(countProjectiles);

        if(countProjectiles <= 0)
        {
            // cannon.RemoveProjectile();
            
            resultPanel.SetActive(true);
            resultPanel.GetComponent<UI_ResultPanel>().Show(UI_ResultPanel.E_Result.Lose, score);
        }
    }

    public void AddScore()
    {
        score++;
        UpdateScore?.Invoke(score);
    }
}
