using Spellsword;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FrostTrap : AbilityBase
{
    public GameObject frostTrapPrefab;
    public GameObject currentTrap = null;
    private float armingTime = 5f;
    private static bool isArmed = false;
    private static bool isSpringed = false;

    public override void PerformAbility(CharacterBase character, bool isPlayer)
    {
        Vector3 playerPosition = GameManager.Instance._playerController.transform.position;
        Vector3 spawnPosition = new Vector3(playerPosition.x, 0f, playerPosition.z);

        if (currentTrap != null && !isSpringed)//if trap spawned and not springed move trap 
        {
            currentTrap.transform.position = spawnPosition;
            ResetTrap();
            Debug.Log("moved... waiting...");
        }
        else// spawn at players feet
        {            
            currentTrap = Instantiate(frostTrapPrefab, spawnPosition, Quaternion.identity); 
            ResetTrap();
            Debug.Log("waiting...");
        }
        Cast();
        base.PerformAbility(character, isPlayer);
    }
    void ResetTrap()
    {
        StopAllCoroutines();
        isArmed = false;
        isSpringed = false;
        StartCoroutine(ArmTrap());
        
    }
    IEnumerator ArmTrap()
    {
        yield return new WaitForSeconds(armingTime);
        isArmed = true;
        Debug.Log("done");
    }
    void SpringTrap(Collider affectedCollider)
    {
        isSpringed = true;
        Debug.Log("step 4");
        
        // ice stuff here
        CharacterBase affectedCharacter = affectedCollider.GetComponent<CharacterBase>();

        StunStatusEffect stunEffect = new StunStatusEffect();
        stunEffect.ApplyEffect(affectedCharacter, 2.5f, 2.5f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if (isArmed && !isSpringed)
            {
                Debug.Log("Springed!");
                SpringTrap(other);
            }
        }
        
    }


}
