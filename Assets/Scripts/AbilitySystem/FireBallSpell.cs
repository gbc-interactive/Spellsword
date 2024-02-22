using Spellsword;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class FireBallSpell : AbilityBase
{
    private SphereCollider sphereCollider;
    public float maxChargeTime = 5f;
    private float rate = 0.0001f;
    public GameObject fireballPrefab;
    public GameObject fireCirclePrefab;
    private GameObject fireballInstance;
    private Vector3 targetPosition;
    public enum Damage
    {
        Small,
        Medium,
        Large
    }
    public Damage DamageScale()
    {
        if (chargeTime <= maxChargeTime / 3)
        {
            return Damage.Small;
        }
        else if (chargeTime <= (2 * maxChargeTime) / 3)
        {
            return Damage.Medium;
        }
        else
        {
            return Damage.Large;
        }
    }    
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isCharging)
        {
            if (fireballInstance == null)//spawn the fireball only once 
            {   
                fireballInstance = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                Rigidbody rb = fireballInstance.GetComponent<Rigidbody>();
                rb.useGravity = false;
            }            
            
            FireBall();
        }
        if (fireballInstance != null)
        {
            float groundThreshold = 0.01f;
            if (fireballInstance.transform.position.y <= groundThreshold)
            {
                InstantiateFireCircle();
                Destroy(fireballInstance);
                fireballInstance = null;
                sphereCollider.radius = 0.1f;
                
            }
        }
        CooldownManagement();//have to include this here to check cooldown status for the ability but not for the other abilities. why? am i dumb 
    }
    void InstantiateFireCircle()
    {
        GameObject circle = Instantiate(fireCirclePrefab, fireballInstance.transform.position, Quaternion.identity);
        float radius = sphereCollider.radius;
        circle.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);//make the ball bigger
        StartCoroutine(DestroyAfterTime(circle, 5f));
        
    }
    public override void PerformAbility()
    {
        Cast();
        
        ThrowFireball(fireballInstance);
        base.PerformAbility();
    }
    void FireBall()
    {

        if (chargeTime < maxChargeTime)
        {
            chargeTime += Time.deltaTime;
            sphereCollider.radius += rate * chargeTime;
            float radius = sphereCollider.radius;
            fireballInstance.transform.localScale = new Vector3(radius, radius, radius);//make the ball bigger
        }

    }
    public void ThrowFireball(GameObject fireball)
    {
        Damage damageScale = DamageScale();
        switch (damageScale)
        {
            case Damage.Small:
                // Apply small damage
                break;
            case Damage.Medium:
                // Apply medium damage
                break;
            case Damage.Large:
                // Apply large damage
                break;
        }
        SphereCollider sc;
        sc = fireball.AddComponent<SphereCollider>();
        sc.radius = sphereCollider.radius;
        sc.isTrigger = true;
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        if (plane.Raycast(ray, out float enter))
        {
            Vector3 worldPosition = ray.GetPoint(enter);
            targetPosition = new Vector3(worldPosition.x, 0, worldPosition.z);
        }

        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.useGravity = true;
        Vector3 direction = (targetPosition - fireball.transform.position);
        float distance = direction.magnitude - fireball.transform.localScale.x - 0.35f;
        float angle = 45f; // Angle for maximum distance
        float velocity = Mathf.Sqrt(distance * -Physics.gravity.y / Mathf.Sin(Mathf.Deg2Rad * angle * 2));
        direction.y = distance * Mathf.Tan(Mathf.Deg2Rad * angle);
        rb.velocity = velocity * direction.normalized;
    }

    IEnumerator DestroyAfterTime(GameObject objectToDestroy, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(objectToDestroy);
    }
}
