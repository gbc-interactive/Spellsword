using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private float duration;
    public ParticleSystem breakParticles;
    public void Break()
    {
        if(breakParticles != null)
        {
            Instantiate(breakParticles, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Break Particles not set");
            Destroy(gameObject);
        }

        if (duration > 0)
        {
            Destroy(gameObject,duration);
        }
        else
        {            
            Destroy(gameObject);
        }
        
    }

}
