using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;
using UnityEngine.AI;

public class MoveAwayFromPlayerBehaviour : BaseAIBehaviour 
{
    public override void UpdateBehaviour(EnemyBehaviour _enemySelf)
    {
        Vector3 desiredSpace;
        float distance = 4.0f;

        Vector3 playerPosition = _enemySelf._getPlayerTarget.transform.position;
        Vector3 awayFromPlayerDirection = (_enemySelf.transform.position - playerPosition).normalized;

        //Check if away from player direction is valid
        NavMeshHit hit;
        desiredSpace = _enemySelf.transform.position + awayFromPlayerDirection * distance;

        bool isDestinationBlocked = _enemySelf._navAgent.Raycast(desiredSpace, out hit);

        if (!isDestinationBlocked)
        {
            _enemySelf._navAgent.SetDestination(desiredSpace);
            return;
        }
    }

}
