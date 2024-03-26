using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private float _duration;
    public ParticleSystem _breakParticles;
    public void Break()
    {
        if(_breakParticles != null)
        {
            Instantiate(_breakParticles, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Break Particles not set");
            Destroy(gameObject);
        }

        if (_duration > 0)
        {
            Destroy(gameObject,_duration);
        }
        else
        {            
            Destroy(gameObject);
        }
        
    }

}
