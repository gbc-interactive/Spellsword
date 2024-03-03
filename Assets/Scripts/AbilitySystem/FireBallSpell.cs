using Spellsword;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class FireBallSpell : AbilityBase
{
    private SphereCollider sphereCollider;
    [SerializeField] private float maxChargeTime = 5f;
    [SerializeField] private float initialRadius = 0.25f;
    [SerializeField] private float rate = 0.001f;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject fireCirclePrefab;
    private GameObject fireballInstance;
    private GameObject fireCircleInstance;
    [SerializeField] public static bool isGrounded = false;
    private Vector3 targetPosition;
    private float lifeTime = 2f;
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
    public void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = initialRadius;
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
            if(isGrounded)
            {
                InstantiateFireCircle();
                Destroy(fireballInstance);
                fireballInstance = null;
                sphereCollider.radius = 0.1f;
                isGrounded = false;
            }
            else if (!isCharging)
            {
                lifeTime -= Time.deltaTime;
                Debug.Log(lifeTime);
                if(lifeTime < 0)
                {
                    StartCoroutine(DestroyAfterTime(fireballInstance,0.1f));
                    fireballInstance = null;
                    sphereCollider.radius = 0.1f;
                    isGrounded = false;
                    lifeTime = 2f;
                }
                
            }
            
        }

        CooldownManagement();
    }
    
    public void InstantiateFireCircle()
    {
        fireCircleInstance = Instantiate(fireCirclePrefab, fireballInstance.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        float radius = sphereCollider.radius;
        fireCircleInstance.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);//make the ball bigger
        StartCoroutine(DestroyAfterTime(fireCircleInstance, 5f));        
    }
    public override void PerformAbility(CharacterBase character, bool isPlayer)
    {
        Cast();

        ThrowFireball(fireballInstance);
        base.PerformAbility(character, isPlayer);
    }
    void FireBall()
    {
        fireballInstance.transform.position = transform.position;
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
        //fireball.transform.parent = null;
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
        fireball.AddComponent<FireBallSpell>();
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
