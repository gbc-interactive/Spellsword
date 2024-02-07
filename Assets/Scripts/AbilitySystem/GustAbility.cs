using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Spellsword
{
    public class GustAbility : AbilityBase
    {
        public float gustCDTime = 2.5f;
        public float lastCastTime;
        public float gustRange = 5f;
        public float gustForce = 10f;
        public bool isActive = false;
        void Start()
        {
            lastCastTime = -gustCDTime;
        }

        void Update()
        {
            if (isActive)
            {
                StartCoroutine(CastGust());
            }
        }
        IEnumerator CastGust()
        {
            int playerLayer = LayerMask.NameToLayer("Player");
            int ignoreLayer = LayerMask.NameToLayer("IgnoreLayer");
            int layerMask = ~((1 << playerLayer) | (1 << ignoreLayer)); // will ignore the players layer and other ignore layers

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, gustRange, layerMask);
            foreach (var hitCollider in hitColliders)
            {
                Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = hitCollider.transform.position - transform.position;
                    float distance = direction.magnitude;
                    rb.AddForce(direction.normalized * gustForce / distance, ForceMode.Impulse);
                    //if enemy collide with object get enemy vel  
                    //float damage = collision.relativeVelocity.magnitude;
                }
            }
            isActive = false;
            yield return null;
        }
        //void OnDrawGizmos()//displays range of attack
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(transform.position, gustRange);
        //}
        public override void PerformAbility()
        {
            if (Time.time - lastCastTime < gustCDTime)
            {
                return; // Ability is on cooldown
            }
            isActive = true;
            lastCastTime = Time.time;
            base.PerformAbility();
            
        }
    }
}
