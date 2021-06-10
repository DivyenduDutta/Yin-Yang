using UnityEngine;

public class ParticleSystemAutoDestructor : MonoBehaviour
{
    private ParticleSystem parSys;
    void Update()
    {
        if (parSys == null)
        {
            parSys = GetComponent<ParticleSystem>();
        }

        if (parSys != null && !parSys.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
