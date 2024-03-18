using Spellsword;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlammableObject : MonoBehaviour
{
    //FlammableObject needs a Collider
    public GameObject fireFXPrefab;
    public GameObject destroyFX;
    public GameObject explodeFX;
    [SerializeField] public float burnTime = 5f;
    [SerializeField] public bool isExplosive = false;
    [SerializeField] public float explosionRadius = 5f;
    [SerializeField] public float explosionForce = 1000f;
    private ParticleSystem _particleSystem;
    public bool isBurning = false;    
    private bool isExploding = false;
    private List<string> startFireTags;
    public int _damageValue = 25;
    void Start()
    {
        startFireTags = new List<string> { "Flame", "PlayerFireBall", "EnemyFireBall" };
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flame"))
        {
            if(other.gameObject.transform.localScale.x > 0.5f)
            {
                StartBurn();
            }
        }
        if (isExploding)
        {
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<CharacterBase>().TakeDamage(_damageValue);
            }
            else if (other.gameObject.CompareTag("Breakable"))
            {
                other.GetComponent<BreakableObject>().Break();
            }
            else if (other.gameObject.GetComponent<FlammableObject>())
            {
                other.GetComponent<FlammableObject>().StartBurn();
            }
        }
    }

    void StartBurn()
    {
        isBurning = true;
        _particleSystem = Instantiate(fireFXPrefab, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        _particleSystem.transform.parent = transform;
        _particleSystem.Play();
        Invoke(nameof(EndBurn), burnTime);
    }
    private void EndBurn()
    {
        if(isExplosive)
        {
            StartCoroutine(Explode());
        }
        else
        {
            Instantiate(destroyFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        isBurning = false;
    }

    IEnumerator Explode()
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
        Instantiate(explodeFX, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.25f);
        isExploding = false;
        Destroy(gameObject);
    }
}
