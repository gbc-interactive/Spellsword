using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Spellsword
{
    public class GustAbility : AbilityBase
    {
        public float gustRange = 5f;
        public float gustForce = 10f;
        public bool isActive = false;
        public override void PerformAbility()
        {

            isActive = true;
            Cast();
            base.PerformAbility();
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

            Collider[] hitColliders = Physics.OverlapSphere(GameManager.Instance._playerController.transform.position, gustRange, layerMask);
            foreach (var hitCollider in hitColliders)
            {
                Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = hitCollider.transform.position - GameManager.Instance._playerController.transform.position;
                    float distance = direction.magnitude;
                    rb.AddForce(direction.normalized * gustForce / distance, ForceMode.Impulse);
                    //if enemy collide with object get enemy vel  
                    //float damage = collision.relativeVelocity.magnitude;
                }
            }
            isActive = false;
            Debug.Log("Done");
            yield return null;
        }

        //void OnDrawGizmos()//displays range of attack
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(GameManager.Instance._playerController.transform.position, gustRange);
        //}
        
    }
}
