using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class MeleeAbility : AbilityBase
    {
        private Vector3 originalScale;
        private float dashDistance = 10.0f;
        private float dashCDTime = 0.5f;
        private float lastCastTime;

        public int _damageValue = 25;

        [SerializeField] private ParticleSystem hitFX;

        void Start()
        {
            originalScale = _particleSystem.gameObject.transform.localScale;
            lastCastTime = -dashCDTime;
        }

        public override void PerformAbility(CharacterBase character, bool isPlayer)
        {
            if (Time.time - lastCastTime < dashCDTime)
            {
                return; // Ability is on cooldown
            }

            lastCastTime = Time.time;
            if (isPlayer)
            {
                //get target direction based on mouse position
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                Plane plane = new Plane(Vector3.up, new Vector3(0, 0.7f, 0)); // Plane at the player's position y

                if (plane.Raycast(ray, out float enter))
                {
                    Vector3 worldPosition = ray.GetPoint(enter);
                    Vector3 direction = worldPosition - GameManager.Instance._playerController.transform.position;
                    direction.Normalize();

                    GameManager.Instance._playerController.TryMove(direction * dashDistance);

                    // Calculate the rotation to align the y-axis with the direction vector
                    Quaternion rotation = Quaternion.LookRotation(direction);

                    // Apply the rotation to the _particleSystem's transform
                    _particleSystem.gameObject.transform.rotation = rotation;
                }
            }
            else
            {
                //get target direction based on player position
                Vector3 directionToPlayer = GameManager.Instance._playerController.transform.position - transform.position;
                directionToPlayer.Normalize();

                _particleSystem.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, directionToPlayer.y, directionToPlayer.z));
            }
            StartCoroutine(CastMeleeAura());
            base.PerformAbility(character, isPlayer);
        }

        IEnumerator CastMeleeAura()
        {
            GetComponent<SphereCollider>().enabled = true;
            yield return new WaitForSeconds(0.25f);
            GetComponent<SphereCollider>().enabled = false;
        }

        public override void HandleCollision(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<CharacterBase>().TakeDamage(_damageValue);
                Vector3 spawnPos = new Vector3(other.transform.position.x, 0, other.transform.position.z);
                Instantiate(hitFX, spawnPos, other.transform.rotation);
            }
        }
    }
}