using Spellsword;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class FrostTrap : AbilityBase
{
    public GameObject frostTrapPrefab;
    public GameObject iceSheetPrefab;
    public GameObject currentTrap = null;
    private float armingTime = 5f;
    private static bool isArmed = false;
    private static bool isSpringed = false;

    public override bool PerformAbility(CharacterBase character, bool isPlayer)
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
        if (isPlayer)
        {
            UIManager.Instance._headsOverDisplay.StartCooldown(2, _cooldownTime);
        }
        character._timeSinceLastAbility = 0;
        return true;
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
            if (isArmed && !isSpringed)
            {
                Debug.Log("Springed!");
                SpringTrap(other);
            }
        }
        
    }


}
