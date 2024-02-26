using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public class FireballAttackBehaviour : BaseAIBehaviour
{
    public override void UpdateBehaviour(EnemyBehaviour _enemySelf)
    {
        MagicEnemyBehaviour magicEnemy = _enemySelf as MagicEnemyBehaviour;


        if (magicEnemy.fireballAttack.chargeUpCurrentCount < magicEnemy.fireballAttack.chargeUpMaxCount)
        {
            magicEnemy.fireballAttack.chargeUpCurrentCount += Time.fixedDeltaTime;
        }
        else
        {
            magicEnemy.PerformAbility(magicEnemy.fireballAttack.ability);
            magicEnemy.fireballAttack.cooldownCurrentCount = 0.0f;
            magicEnemy.fireballAttack.chargeUpCurrentCount = 0.0f;
        }

    }

}
