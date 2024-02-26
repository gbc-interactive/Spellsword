using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public class RangedAttackBehaviour : BaseAIBehaviour
{
    public override void UpdateBehaviour(EnemyBehaviour _enemySelf)
    {
        RangedEnemyBehaviour rangedEnemy = _enemySelf as RangedEnemyBehaviour;

        if (rangedEnemy.rangedAttack.chargeUpCurrentCount < rangedEnemy.rangedAttack.chargeUpMaxCount)
            rangedEnemy.rangedAttack.chargeUpCurrentCount += Time.fixedDeltaTime;
        else
        {
            rangedEnemy.PerformAbility(rangedEnemy.rangedAttack.ability);
            rangedEnemy.rangedAttack.chargeUpCurrentCount = 0.0f;
            rangedEnemy.rangedAttack.cooldownCurrentCount = 0.0f;
        }

    }

}
