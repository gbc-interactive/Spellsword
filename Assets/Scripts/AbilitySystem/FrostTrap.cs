using Spellsword;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FrostTrap : AbilityBase
{
    public GameObject frostTrapPrefab;
    public GameObject currentTrap = null;
    private float armingTime = 5f;
    private static bool isArmed = false;
    private static bool isSpringed = false;

    public override void PerformAbility()
    {
        Vector3 playerPosition = GameManager.Instance._playerController.transform.position;
        Vector3 spawnPosition = new Vector3(playerPosition.x, playerPosition.y - 0.7f, playerPosition.z);

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
        base.PerformAbility();
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
    void SpringTrap()
    {
        isSpringed = true;
        Debug.Log("step 4");
        //ice stuff here
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if (isArmed && !isSpringed)
            {
                Debug.Log("Springed!");
                SpringTrap();
            }
        }
        
    }


}