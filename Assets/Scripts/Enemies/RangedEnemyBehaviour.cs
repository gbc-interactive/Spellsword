using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class RangedEnemyBehaviour : EnemyBehaviour
    {
        [HideInInspector] public AbilityForAI rangedAttack;

        protected override void Start()
        {
            base.Start();
            rangedAttack = _abilities[0];
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

            if (rangedAttack.cooldownCurrentCount >= rangedAttack.cooldownMaxCount)
            {
                SwitchBehaviour(BehavioursAI.rangedAttack);
                return;
            }

            Vector3 targetPosition = _getPlayerTarget.transform.position;
            Vector3 enemySelfPosition = transform.position;
                    break;

                case EBehaviours.FoundPlayer:
                    BehavioursAI.FoundPlayer(this);
                    break;

                case EBehaviours.MoveToPlayer:
                    BehavioursAI.MoveToPlayer(this);
                    break;

            bool isEnemyTooFarFromPlayer = Vector3.Distance(targetPosition, enemySelfPosition) > _safeZoneDistanceMax;
            bool isEnemyTooCloseToPlayer = Vector3.Distance(targetPosition, enemySelfPosition) < _safeZoneDistanceMin;

            if (isEnemyTooFarFromPlayer)
                SwitchBehaviour(BehavioursAI.moveToPlayer);
            else if (isEnemyTooCloseToPlayer)
                SwitchBehaviour(BehavioursAI.moveAwayFromPlayer);
            else
                SwitchBehaviour(BehavioursAI.circlePlayer);

        }
    }
}