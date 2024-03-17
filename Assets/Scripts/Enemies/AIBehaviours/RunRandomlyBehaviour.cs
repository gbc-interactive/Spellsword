using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public class RunRandomlyBehaviour : BaseAIBehaviour
{
    

    public override void EnterBehaviour(EnemyBehaviour _enemySelf)
    {
        Vector3 enemyPosition = _enemySelf.transform.position;


        float x, z;

        x = Random.Range(-1.0f, 1.0f);
        z = Random.Range(-1.0f, 1.0f);

        // Random Direction X       and       Random Direction Z
        Vector3 runDirection = new Vector3(x, 0, z);

        RaycastHit hit;
        if (!Physics.Raycast(enemyPosition, runDirection, out hit, 10.0f))
        {
            x = enemyPosition.x + x * 10.0f;
            z = enemyPosition.z + z * 10.0f;

            Vector3 runPoint = new Vector3(x, _enemySelf.transform.position.y, z);
            _enemySelf._randomPosition = runPoint;
        }

    }

    public override void UpdateBehaviour(EnemyBehaviour _enemySelf)
    {
        bool doesEnemyHaveRandomPosition = _enemySelf._randomPosition != null;
        bool isEnemyAtRandomPosition = Vector3.Distance(_enemySelf.transform.position, _enemySelf._randomPosition) < 3.0f;

        if (!doesEnemyHaveRandomPosition || isEnemyAtRandomPosition)
            EnterBehaviour(_enemySelf);
        else
            _enemySelf._navAgent.SetDestination(_enemySelf._randomPosition);
    }

    public override void ExitBehaviour(EnemyBehaviour _enemySelf)
    {
        _enemySelf._randomPosition = Vector3.zero;
    }
}
