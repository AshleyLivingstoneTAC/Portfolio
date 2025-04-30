using Unity.VisualScripting;
using UnityEngine;

public class PlanetList : MonoBehaviour
{
    public Planet[] planets;
    public Planet currentPlanet;

    private void Start()
    {
        planets = FindObjectsByType<Planet>(FindObjectsSortMode.None);
        currentPlanet = planets[0];
    }

    private void Update()
    {
        
    }

}
