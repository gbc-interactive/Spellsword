using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spellsword
{
    public class FireBall : MonoBehaviour
    {
        public bool isGrounded = false;
        [SerializeField] private GameObject fireCirclePrefab;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
                InstantiateFireCircle();
            }
        }

        public void InstantiateFireCircle()
        {
            GameObject fireCircleInstance = Instantiate(fireCirclePrefab, gameObject.transform.position, Quaternion.Euler(0f, 0f, 0f));
            float radius = GetComponent<SphereCollider>().radius;
            fireCircleInstance.transform.localScale = new Vector3(radius * 2.5f, radius * 2.5f, radius * 2.5f);//make the ball bigger
            Destroy(gameObject);
        }
    }

}

