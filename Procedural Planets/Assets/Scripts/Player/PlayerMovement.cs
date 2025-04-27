using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 pos;
    public float mass;

    public float force;
    float G = 6.674e-11f;
    float fakeG = 0.25f;
    public Planet nearestPlanet;
    public Camera cam;
    public Vector3 camDistance;
    public Vector3 r;
    public float rMag;
    private void Start()
    {
        pos = transform.position;
    }

    private void Update()
    {
        
        MoveOnInput();
        GravitationalPull();
        
        rMag = r.magnitude;
        transform.position = pos;
        pos = transform.position;

        Debug.Log(pos);
    }

    public void MoveOnInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            pos.z += 0.1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += 0.1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.z -= 0.1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= 0.1f;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            pos.y += 1;
        }
    }

    public void GravitationalPull()
    {
        
        force = (fakeG * mass * nearestPlanet.mass) / r.sqrMagnitude;
        pos -= r * force;
        r = transform.position - nearestPlanet.transform.position;
        //transform.rotation = Quaternion.LookRotation(r);
    }
}
