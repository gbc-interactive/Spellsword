using Spellsword;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlammableObject : MonoBehaviour
{
    //FlammableObject Needs a Rigidbody and a Collider
    public GameObject fireFXPrefab; 
    [SerializeField] public float burnTime = 5f;
    [SerializeField] public bool isExplosive = false;
    [SerializeField] public float explosionRadius = 5f;
    [SerializeField] public float explosionForce = 700f;
    private ParticleSystem _particleSystem;
    public bool isBurning = false;    
    private bool isExploding = false;

    public int _damageValue = 25;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flame"))
        {
            StartBurn();
        }
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            if (isExploding)
            {
                other.GetComponent<CharacterBase>().TakeDamage(_damageValue);
                Destroy(gameObject);
            }
        }
    }
    void StartBurn()
    {
        isBurning = true;
        _particleSystem = Instantiate(fireFXPrefab, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        _particleSystem.transform.parent = transform;
        _particleSystem.Play();
        Invoke("EndBurn", burnTime);
    }
    private void EndBurn()
    {
        if(isExplosive)
        {
            Explode();
        }
        else
        {
            Destroy(gameObject);
        }
        isBurning = false;
    }
    void Explode()
    {
        SphereCollider explosionTrigger = gameObject.AddComponent<SphereCollider>();
        explosionTrigger.radius = explosionRadius;
        explosionTrigger.isTrigger = true;
        isExploding = true;
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
}
