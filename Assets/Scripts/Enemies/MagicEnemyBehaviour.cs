using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class MagicEnemyBehaviour : EnemyBehaviour
    {
        [HideInInspector] public AbilityForAI fireballAttack;
        [HideInInspector] public AbilityForAI teleportAbility;

        protected override void Start()
        {
            base.Start();
            fireballAttack = _abilities[0];
            teleportAbility = _abilities[1];
        }

        protected override void DetermineBehaviour()
        {
            foreach (StatusEffect effect in statusEffects)
            {
                if (effect is BurnStatusEffect)
                {
                    SwitchBehaviour(BehavioursAI.runRandomly);
                    return;
                }
            }

            if (_getPlayerTarget == null)
            {
                SwitchBehaviour(BehavioursAI.moveToHome);
                return;
            }

            if (fireballAttack.cooldownCurrentCount >= fireballAttack.cooldownMaxCount)
            {
                SwitchBehaviour(BehavioursAI.fireballAttack);
                return;
            }

            Vector3 targetPosition = _getPlayerTarget.transform.position;
            Vector3 enemySelfPosition = transform.position;

            bool isEnemyTooFarFromPlayer = Vector3.Distance(targetPosition, enemySelfPosition) > _safeZoneDistanceMax;
            bool isEnemyTooCloseToPlayer = Vector3.Distance(targetPosition, enemySelfPosition) < _safeZoneDistanceMin;

            if (isEnemyTooFarFromPlayer)
                SwitchBehaviour(BehavioursAI.moveToPlayer);

            else if (isEnemyTooCloseToPlayer)
            {
                if (teleportAbility.cooldownCurrentCount >= teleportAbility.cooldownMaxCount)
                    SwitchBehaviour(BehavioursAI.teleport);
                else
                    SwitchBehaviour(BehavioursAI.moveAwayFromPlayer);
            }
            else
                SwitchBehaviour(BehavioursAI.circlePlayer);
        }
    }
}
