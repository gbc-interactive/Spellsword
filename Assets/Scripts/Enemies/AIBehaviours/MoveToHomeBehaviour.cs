using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public class MoveToHomeBehaviour : BaseAIBehaviour 
{
    public override void UpdateBehaviour(EnemyBehaviour _enemySelf)
    {
        bool doesEnemyHaveHome = _enemySelf._homePosition != null;
        bool isEnemyAtHome = Vector3.Distance(_enemySelf.transform.position, _enemySelf._homePosition) < 3.0f;

        if (!doesEnemyHaveHome || isEnemyAtHome)
            BehavioursAI.idle.UpdateBehaviour(_enemySelf);
        else
            _enemySelf._navAgent.SetDestination(_enemySelf._homePosition);
    }
}
