using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Spellsword;
using UnityEngine;

public class TeleportBehaviour : BaseAIBehaviour 
{
    public override void UpdateBehaviour(EnemyBehaviour _enemySelf)
    {
        MagicEnemyBehaviour magicEnemy = _enemySelf as MagicEnemyBehaviour;

        Vector3 enemyPosition = magicEnemy.transform.position;
        Vector3 playerPosition = _enemySelf._getPlayerTarget.transform.position;

        Vector3 playerDirection = (enemyPosition - playerPosition).normalized;
        Vector3 teleportVector = new Vector3(playerDirection.x, -0.125f, playerDirection.z);

        RaycastHit hit;
        if (Physics.Raycast(_enemySelf.transform.position, teleportVector, out hit, 10.0f))
        {
            if (hit.collider.gameObject.name == "GroundMesh")
            {
                magicEnemy.PerformAbility(magicEnemy.teleportAbility.ability, false);
                magicEnemy.transform.position = new Vector3(hit.point.x, magicEnemy.transform.position.y, hit.point.z);
                magicEnemy.teleportAbility.cooldownCurrentCount = 0.0f;
            }
            else
            {
                BehavioursAI.moveAwayFromPlayer.UpdateBehaviour(magicEnemy);
            }
        }
    }

}
