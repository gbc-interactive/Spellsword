using Spellsword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class ArrowAbility : AbilityBase
{
    private float speed = 10f;
    private Vector3 direction;
    public GameObject arrowPrefab;
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
    private void SetArrowDirection(Vector3 startPosition, Vector3 targetPosition)
    {
        transform.position = startPosition;
        direction = (targetPosition - startPosition).normalized;
        direction.y = 0;

        arrowInstance.transform.LookAt(targetPosition);
    }
    private void ShootArrow(bool isPlayer)
    {
        arrowInstance = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        StartCoroutine(DestroyAfterTime(arrowInstance, 2f));
        if (isPlayer)
        {
            //SetArrowDirection(GameManager.Instance._playerController.transform.position,enemyposition );
        }
        else
        {
            SetArrowDirection(transform.position, GameManager.Instance._playerController.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (arrowInstance != null)
        {
            arrowInstance.transform.position += direction * speed * Time.deltaTime;
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
