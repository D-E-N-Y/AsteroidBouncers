using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem current;

    [SerializeField] private List<Planet> planets;
    private int currentPlanet;
    
    [SerializeField] private Cannon cannon;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        currentPlanet = 3;
        
        planets[currentPlanet].gameObject.SetActive(true);
        planets[currentPlanet].Initialize();

        cannon.gameObject.SetActive(true);
        cannon.Initialize(planets[currentPlanet].GetColors());
    }

    public void NextPlanet()
    {
        planets[currentPlanet].gameObject.SetActive(false);
        
        currentPlanet++;
        planets[currentPlanet].gameObject.SetActive(true);
        planets[currentPlanet].Initialize();

        cannon.Initialize(planets[currentPlanet].GetColors());
    }
}
