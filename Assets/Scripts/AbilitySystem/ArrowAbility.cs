using Spellsword;
using System.Collections;
using UnityEngine;

namespace Spellsword
{
    public class ArrowAbility : AbilityBase
    {
        private float _speed = 10f;
        private Vector3 _direction;
        [SerializeField] private GameObject arrowPrefab;
        private GameObject arrowInstance;
        private Rigidbody rb;
        public override bool PerformAbility(CharacterBase character, bool isPlayer)
        {
            Cast();

            ShootArrow(isPlayer);
            rb = arrowInstance.GetComponent<Rigidbody>();
            base.PerformAbility(character, isPlayer);
            return true;
        }
        private void SetArrow_direction(Vector3 startPosition, Vector3 _targetPosition)
        {
            transform.position = startPosition;
            _direction = (_targetPosition - startPosition).normalized;
            _direction.y = 0;

            arrowInstance.transform.LookAt(_targetPosition);
        }
        private void ShootArrow(bool isPlayer)
        {
            arrowInstance = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            StartCoroutine(DestroyAfterTime(arrowInstance, 2f));
            if (isPlayer)
            {
                //SetArrow_direction(GameManager.Instance._playerController.transform.position,enemyposition );
            }
            else
            {
                SetArrow_direction(transform.position, GameManager.Instance._playerController.transform.position);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (arrowInstance != null)
            {
                arrowInstance.transform.position += _direction * _speed * Time.deltaTime;
                rb.AddForce(Vector3.down * 1.0f, ForceMode.Force);
                arrowInstance.transform.Rotate(20.0f * Time.deltaTime, 0, 0);
            }
            CooldownManagement();
        }

        IEnumerator DestroyAfterTime(GameObject objectToDestroy, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(objectToDestroy);
        }
    }
}


