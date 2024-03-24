using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace Spellsword
{
    public class FireBall : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private GameObject vaporCloudPrefab;
        [SerializeField] private GameObject fireCirclePrefab;
        public bool isGrounded = false;
        public Vector3 offset = new Vector3(0, 1f, 0);//for spawning cloud
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
                InstantiateFireCircle();
            }
            else if (other.gameObject.CompareTag("IceSheet"))
            {
                GameObject cloud = Instantiate(vaporCloudPrefab, transform.position + offset, Quaternion.identity);
                cloud.gameObject.GetComponent<BreakableObject>().Break();
            }
        }

        public void InstantiateFireCircle()
        {
            GameObject fireCircleInstance = Instantiate(fireCirclePrefab, gameObject.transform.position, Quaternion.Euler(0f, 0f, 0f));
            float radius = GetComponent<SphereCollider>().radius;
            fireCircleInstance.transform.localScale = new Vector3(radius * 2.5f, radius * 2.5f, radius * 2.5f);//make the ball bigger
            Destroy(gameObject);
        }
        void Update()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}

