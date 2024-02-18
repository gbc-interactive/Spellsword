using Spellsword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpell : AbilityBase
{
    private SphereCollider sphereCollider;
    private float rate = 0.1f;
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        
    }
    public override void PerformAbility()
    {
        Cast();
        FireBall();
        
        Debug.Log("casting fireball");
        base.PerformAbility();
    }
    void FireBall()
    {
        rate += Time.deltaTime;
        sphereCollider.radius += rate;
        Debug.Log("YEah dawg");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
