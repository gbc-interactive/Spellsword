using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Spellsword
{
    public class MeleeAbility : AbilityBase
    {
        private Vector3 originalScale;
        private float dashForce = 15.0f;
        private float knockBackForce = 15.0f;

        public int _damageValue = 25;

        [SerializeField] private ParticleSystem hitFX;

        void Start()
        {
            originalScale = _particleSystem.gameObject.transform.localScale;
        }

        public override bool PerformAbility(CharacterBase character, bool isPlayer)
        {
            Cast();
            Dash(isPlayer);
            character.characterState = EAnimationState.Melee;
            character.SetAnimation();
            base.PerformAbility(character, isPlayer);
            return true;
        }
        void Dash(bool isPlayer)
        {
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

                    GameManager.Instance._playerController.GetComponent<Rigidbody>().AddForce(direction * dashForce, ForceMode.Impulse);

                    if(direction.x < 0)
                    {
                        GameManager.Instance._playerController.SetFacingDirection(EDirection.Right);
                        _particleSystem.gameObject.transform.localScale = new Vector3(Mathf.Abs(_particleSystem.gameObject.transform.localScale.x) * -1, _particleSystem.gameObject.transform.localScale.y, _particleSystem.gameObject.transform.localScale.z);
                    }
                    else
                    {
                        GameManager.Instance._playerController.SetFacingDirection(EDirection.Left);
                        _particleSystem.gameObject.transform.localScale = new Vector3(Mathf.Abs(_particleSystem.gameObject.transform.localScale.x), _particleSystem.gameObject.transform.localScale.y, _particleSystem.gameObject.transform.localScale.z);
                    }
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
        }

        IEnumerator CastMeleeAura()
        {
            yield return new WaitForSeconds(_particleSystem.main.startDelay.constant);
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

                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = other.transform.position - transform.position;
                    rb.AddForce(direction.normalized * knockBackForce, ForceMode.Impulse);
                }
            }
        }
    }
}