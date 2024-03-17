using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public ParticleSystem breakParticles;

    public void Break()
    {
        Instantiate(breakParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
