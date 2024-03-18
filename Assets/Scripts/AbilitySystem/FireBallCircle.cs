using Spellsword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCircle : MonoBehaviour
{
    public float timeToDestroy = 5f;
    public int _damageValue = 15;
    private float knockBackForce = 15.0f;
    public ParticleSystem fireCircle;

    void OnEnable()
    {
        StartCoroutine(DestroyAfterTime(timeToDestroy));
    }

    IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<SphereCollider>().enabled = false;
        fireCircle.Stop();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player") && transform.localScale.x > 0.5f)
        {
            other.GetComponent<CharacterBase>().TakeDamage(_damageValue);

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = other.transform.position - transform.position;
                rb.AddForce(direction.normalized * knockBackForce, ForceMode.Impulse);
            }
        }
    }
}
