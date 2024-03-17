using Spellsword;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FireBallSpell : AbilityBase
{
    private SphereCollider sphereCollider;
    [SerializeField] private float maxChargeTime = 5f;
    [SerializeField] private float initialRadius = 0.25f;
    [SerializeField] private float rate = 0.0001f;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject fireCirclePrefab;
    private GameObject fireballInstance;
    private GameObject fireCircleInstance;
    [SerializeField] public static bool isGrounded = false;
    private Vector3 targetPosition;
    private float lifeTime = 2f;
    public int _damageValue;   
    private bool isCasting = false;
    public void Start()
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
                fireballInstance = Instantiate(fireballPrefab,transform.position, Quaternion.identity);
                Rigidbody rb = fireballInstance.GetComponent<Rigidbody>();
                rb.useGravity = false;
            }            
            
            MakeBallBigger();
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
            else if (!isCharging && isCasting)
            {
                lifeTime -= Time.deltaTime;
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
        fireCircleInstance = Instantiate(fireCirclePrefab, fireballInstance.transform.position, Quaternion.Euler(0f, 0f, 0f));
        float radius = sphereCollider.radius;
        fireCircleInstance.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);//make the ball bigger
        StartCoroutine(DestroyAfterTime(fireCircleInstance, 5f));
        Invoke("ResetDamageValue", 5f);
    }
    void ResetDamageValue()
    {
        _damageValue = 0;
    }
    public override bool PerformAbility(CharacterBase character, bool isPlayer)
    {
        SetDamageScale();
        Cast();//cast resets chargetime to 0 which is why set dmg first        
        ThrowFireball(fireballInstance);
        base.PerformAbility(character, isPlayer);
        if (isPlayer)
        {
            UIManager.Instance._headsOverDisplay.StartCooldown(1, _cooldownTime);
        }
        character._timeSinceLastAbility = 0;
        return true;
    }
    void MakeBallBigger()
    {
        fireballInstance.transform.position = transform.position;
        if (chargeTime < maxChargeTime)
        {
            chargeTime += Time.deltaTime;
            sphereCollider.radius += rate;
            float radius = sphereCollider.radius;
            fireballInstance.transform.localScale = new Vector3(radius, radius, radius);//make the ball bigger
        }

    }
    
    public void ThrowFireball(GameObject fireball)
    {
        isCasting = true;
        SphereCollider sc; 
        fireball.AddComponent<FireBallSpell>();
        sc = fireball.AddComponent<SphereCollider>();
               
        sc.radius = sphereCollider.radius;
        sc.isTrigger = true;
        //sc.tag = "PlayerFireBall";
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
    void SetDamageScale()
    {
        if(chargeTime < 1.2f)
        {
            _damageValue = 10;
        }
        else
        {
            if(chargeTime < 2.5f)
            {
                _damageValue = 20;
            }
            else
            {
                _damageValue = 30;
            }
        }
    }
    
    IEnumerator DestroyAfterTime(GameObject objectToDestroy, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(objectToDestroy);
    }
    public override void HandleCollision(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<CharacterBase>().TakeDamage(_damageValue);
            
        }
    }
}
