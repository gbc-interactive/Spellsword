using Spellsword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Spellsword
{

    public class MeleeEnemyBehaviour : EnemyBehaviour
    {
        [HideInInspector] public AbilityForAI meleeAttack;

        protected override void Start()
        {
            base.Start();
            meleeAttack = _abilities[0];

            StunStatusEffect stunStatusEffect = new StunStatusEffect();
            stunStatusEffect.ApplyEffect(this, 5.0f, 5.0f);
        }

        protected override void DetermineBehaviour()
        {
            if (_getPlayerTarget == null)
            {
                SwitchBehaviour(BehavioursAI.moveToHome);
                return;
            }

            if (meleeAttack.cooldownCurrentCount >= meleeAttack.cooldownMaxCount)
            {
                SwitchBehaviour(BehavioursAI.meleeAttack);
                return;
            }

            Vector3 targetPosition = _getPlayerTarget.transform.position;
            Vector3 enemySelfPosition = transform.position;

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