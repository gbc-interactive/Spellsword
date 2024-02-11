using Spellsword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostTrap : AbilityBase
{
    public GameObject frostTrapPrefab;
    private GameObject currentTrap = null;
    private float armingTime = 5f;
    private bool isArmed = false;
    private bool isSpringed = false;
    public override void PerformAbility()
    {
        Vector3 playerPosition = GameManager.Instance._playerController.transform.position;
        Vector3 spawnPosition = new Vector3(playerPosition.x, playerPosition.y - 0.7f, playerPosition.z);

        //if trap spawned and not springed move trap 
        if (currentTrap != null && !isSpringed)
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
        base.PerformAbility();
    }
    void ResetTrap()
    {
        StopAllCoroutines();
        StartCoroutine(ArmTrap());
        isArmed = false;
        isSpringed = false;
    }
    IEnumerator ArmTrap()
    {
        yield return new WaitForSeconds(armingTime);
        Debug.Log("armed");
        isArmed = true;
    }
    void SpringTrap()
    {
        isSpringed = true;
        Debug.Log("trapped");
        //ice stuff here
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("trigger");
            if (isArmed && !isSpringed)
            {
                SpringTrap();
            }
        }
    }


}
