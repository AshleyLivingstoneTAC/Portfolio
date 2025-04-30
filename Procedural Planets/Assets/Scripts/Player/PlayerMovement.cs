using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 pos;
    public float mass;
    public bool colliding;
    public PlanetList pl;

    public float force;
    float G = 6.674e-11f;
    float fakeG = 0.25f;
    public Planet nearestPlanet;
    public Camera cam;
    public Vector3 camDistance;
    public Vector3 r;
    public float rMag;

    public Vector3 distToP1;
    public Vector3 distToP2;

    private void Start()
    {
        pos = transform.position;
        colliding = false;
    }

    private void Update()
    {
        
         pos += MoveOnInput();
        if (!colliding)
        {
            GravitationalPull();
        }
        rMag = r.magnitude;
        transform.position = pos;
        pos = transform.position;
        transform.rotation = Quaternion.Euler(r);
        distToP1 = transform.position - pl.planets[0].transform.position;
        distToP2 = transform.position - pl.planets[1].transform.position;

        if(distToP1.magnitude < distToP2.magnitude)
        {
            nearestPlanet = pl.planets[0];
        }
        if (distToP1.magnitude > distToP2.magnitude)
        {
            nearestPlanet = pl.planets[1];
        }
    }

    public Vector3 MoveOnInput()
    {
        Vector3 v3 = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            v3.z += 0.1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            v3.x += 0.1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            v3.z -= 0.1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            v3.x -= 0.1f;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            v3.y += 1;
        }
        if (Input.GetKey(KeyCode.P))
        {
            if (nearestPlanet = pl.planets[1])
                nearestPlanet = pl.planets[0];

            if (nearestPlanet = pl.planets[0])
                nearestPlanet = pl.planets[1];
        }
        return v3;
    }

    public void GravitationalPull()
    {
        
        force = (fakeG * mass * nearestPlanet.mass) / r.sqrMagnitude;
        pos -= r * force;
        r = transform.position - nearestPlanet.transform.position;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Planet")
        {
            colliding = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Planet")
        {
            colliding = false;
        }
    }
}
