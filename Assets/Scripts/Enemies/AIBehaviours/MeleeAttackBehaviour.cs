using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public class MeleeAttackBehaviour : BaseAIBehaviour
{
    public override void UpdateBehaviour(EnemyBehaviour _enemySelf)
    {
        MeleeEnemyBehaviour meleeEnemy = _enemySelf as MeleeEnemyBehaviour;
        Vector3 enemyPosition = meleeEnemy.transform.position;
        Vector3 playerPosition = meleeEnemy._getPlayerTarget.transform.position;

        if (Vector3.Distance(enemyPosition, playerPosition) > 1.0f)
            BehavioursAI.moveToPlayer.UpdateBehaviour(meleeEnemy);
        else
        {
            meleeEnemy.PerformAbility(meleeEnemy.meleeAttack.ability, false);
            meleeEnemy.meleeAttack.cooldownCurrentCount = 0.0f;
        }
    }

}
