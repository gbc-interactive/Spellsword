using Spellsword;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

namespace Spellsword
{
    public class FrostTrap : AbilityBase
    {
        public GameObject frostTrapPrefab;
        public GameObject iceSheetPrefab;
        public GameObject currentTrap = null;
        private float _armingTime = 5f;
        private static bool _isArmed = false;
        private static bool _isSpringed = false;

        public override bool PerformAbility(CharacterBase character, bool isPlayer)
        {
            Vector3 playerPosition = GameManager.Instance._playerController.transform.position;
            Vector3 spawnPosition = new Vector3(playerPosition.x, 0f, playerPosition.z);

            if (currentTrap != null && !_isSpringed)//if trap spawned and not springed move trap 
            {
                currentTrap.transform.position = spawnPosition;
                ResetTrap();
            }
            else// spawn at players feet
            {
                currentTrap = Instantiate(frostTrapPrefab, spawnPosition, Quaternion.identity);
                ResetTrap();
            }
            Cast();
            base.PerformAbility(character, isPlayer);
            if (isPlayer)
            {
                UIManager.Instance._headsOverDisplay.StartCooldown(2, _cooldownTime);
            }
            character._timeSinceLastAbility = 0;
            return true;
        }
        void ResetTrap()//REplacing unspringed trap
        {
            StopAllCoroutines();
            _isArmed = false;
            _isSpringed = false;
            StartCoroutine(ArmTrap());
        }
        IEnumerator ArmTrap()//set trap to armed after arming time
        {
            yield return new WaitForSeconds(_armingTime);
            _isArmed = true;
        }
        void SpringTrap(Collider affectedCollider) //spring trap and apply effects. trap is springed
        {
            _isSpringed = true;
            CharacterBase affectedCharacter = affectedCollider.GetComponent<CharacterBase>();
            GameObject icesheet = Instantiate(iceSheetPrefab, transform.position, Quaternion.identity);
            icesheet.GetComponent<BreakableObject>().Break();
            StunStatusEffect stunEffect = new StunStatusEffect();
            stunEffect.ApplyEffect(affectedCharacter, 2.5f, 2.5f);

            Destroy(gameObject);
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
            {
                if (_isArmed && !_isSpringed)
                {
                    Debug.Log("Springed!");
                    SpringTrap(other);
                }
            }

        }
    }
}


