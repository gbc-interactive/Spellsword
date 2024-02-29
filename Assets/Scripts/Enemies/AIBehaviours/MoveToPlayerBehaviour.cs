using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;


public class MoveToPlayerBehaviour : BaseAIBehaviour 
{
    public override void UpdateBehaviour(EnemyBehaviour _enemySelf)
    {
        Vector3 playerPosition = _enemySelf._getPlayerTarget.transform.position;
        _enemySelf._navAgent.SetDestination(playerPosition);
    }
}
