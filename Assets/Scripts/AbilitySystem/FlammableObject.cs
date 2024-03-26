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
    [SerializeField] public float _burnTime = 5f;
    [SerializeField] public bool _isExplosive = false;
    [SerializeField] public float _explosionRadius = 5f;
    [SerializeField] public float _explosionForce = 10000f;
    private ParticleSystem _particleSystem;
    public bool _isBurning = false;    
    private bool _isExploding = false;
    private List<string> _startFireTags;
    private int _fireDamageValue = 15;
    private int _explosionDamageValue = 65;
    void Start()
    {
        _startFireTags = new List<string> { "Flame", "PlayerFireBall", "EnemyFireBall" };
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
        if (_isExploding)
        {
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<CharacterBase>().TakeDamage(_explosionDamageValue);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (_isBurning)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<CharacterBase>().TakeDamage(_fireDamageValue);
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 _direction = collision.gameObject.transform.position - transform.position;
                    rb.AddForce(_direction.normalized * 7.5f, ForceMode.Impulse);
                }
            }
            else if (collision.gameObject.GetComponent<FlammableObject>())
            {
                collision.gameObject.GetComponent<FlammableObject>().StartBurn();
            }
        }
    }

    void StartBurn()
    {
        _isBurning = true;
        _particleSystem = Instantiate(fireFXPrefab, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        _particleSystem.transform.parent = transform;
        _particleSystem.Play();
        Invoke(nameof(EndBurn), _burnTime);
    }
    private void EndBurn()
    {
        if(_isExplosive)
        {
            StartCoroutine(Explode());
        }
        else
        {
            Instantiate(destroyFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        _isBurning = false;
    }

    IEnumerator Explode()
    {
        SphereCollider explosionTrigger = gameObject.AddComponent<SphereCollider>();
        explosionTrigger.radius = _explosionRadius;
        explosionTrigger.isTrigger = true;
        _isExploding = true;
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
        Instantiate(explodeFX, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.25f);
        _isExploding = false;
        Destroy(gameObject);
    }
}
