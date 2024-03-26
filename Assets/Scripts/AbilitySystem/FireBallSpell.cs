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
    private float defaultRadius;
    private SphereCollider sphereCollider;
    [SerializeField] private float maxChargeTime = 5f;
    [SerializeField] private float rate = 0.0001f;
    [SerializeField] private GameObject fireballPrefab;
    private GameObject fireballInstance;
    private Vector3 targetPosition;
    private float lifeTime = 2f;
    private bool isCasting = false;
    public int _damageValue;
    private Rigidbody rb;
    public void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        defaultRadius = sphereCollider.radius;
        if (fireballInstance != null)
        {
            rb = fireballInstance.GetComponent<Rigidbody>();            
        }
    }

    public override void StartCharging()
    {
        if (fireballInstance != null) return;
        sphereCollider.radius = defaultRadius;
        base.StartCharging();
    }
    public override void Update()
    {
        base.Update();
        if (!_isOnCooldown && _isCharging)
        {
            if (fireballInstance == null)//spawn the fireball only once 
            {
                fireballInstance = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                Rigidbody rb = fireballInstance.GetComponent<Rigidbody>();
                rb.useGravity = false;
            }

            MakeBallBigger();
        }
        if (fireballInstance != null)
        {
            if (fireballInstance.GetComponent<FireBall>().isGrounded)
            {
                fireballInstance = null;
                sphereCollider.radius = defaultRadius;
                Invoke(nameof(ResetDamageValue), 5f);
            }
        }
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
        if (_chargeTime < maxChargeTime)
        {
            _chargeTime += Time.deltaTime;
            sphereCollider.radius += rate;
            float radius = sphereCollider.radius;
            fireballInstance.transform.localScale = new Vector3(radius, radius, radius);//make the ball bigger
        }

    }
    
    public void ThrowFireball(GameObject fireball)
    {
        isCasting = true;
        if (fireball == null) return;
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

    void ResetDamageValue()
    {
        _damageValue = 0;
    }

    void SetDamageScale()
    {
        if (_chargeTime < 1.2f)
        {
            _damageValue = 10;
        }
        else
        {
            if (_chargeTime < 2.5f)
            {
                _damageValue = 20;
            }
            else
            {
                _damageValue = 30;
            }
        }
    }
}
