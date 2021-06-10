using UnityEngine;

public class ParticleSystemMove : MonoBehaviour
{
    private Transform ship;

    private void Start()
    {
        ship = GameObject.FindGameObjectWithTag("ship").transform;
    }

    void Update()
    {
        if (ship != null)
        {
            transform.position = ship.position;
        }
        
    }
}
