using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : MonoBehaviour
{
    public float health = 100f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("temptestgust"))
        {
            float damage = collision.relativeVelocity.magnitude;
            health -= damage;
            gameObject.GetComponent<TempEnemy>().health -= damage;
        }
    }
}
