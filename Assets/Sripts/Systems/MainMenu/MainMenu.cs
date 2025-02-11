using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<Planet> planets;
    private int currentPlanet;
    
    void Start()
    {
        currentPlanet = Random.Range(0, planets.Count);
        
        planets[currentPlanet].gameObject.SetActive(true);
        planets[currentPlanet].Initialize();
    }
}
