using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

static public class BehavioursAI
{
        static public void Idle(EnemyBehaviour _enemySelf)
        {
            ChargeAbility(_enemySelf);
        }

        static public void FoundPlayer(EnemyBehaviour _enemySelf)
        {
            if (_enemySelf._getPlayerTarget != null)
            {
                _enemySelf._behaviour = EBehaviours.MoveToPlayer;
            }
        }

        static public void GoToHome(EnemyBehaviour _enemySelf)
        {
            Vector3 enemySelfPosition = _enemySelf.transform.position;

            if (_enemySelf._homePosition != Vector3.zero || Vector3.Distance(_enemySelf._homePosition, enemySelfPosition) > 3.0f)
            {
                _enemySelf._moveVector = (_enemySelf._homePosition - enemySelfPosition).normalized;
            }
            else
            {
                _enemySelf._behaviour = EBehaviours.Idle;
            }
        }

        static public void MeleeAttackPlayer(EnemyBehaviour _enemySelf)
        {
            if (_enemySelf._attackCooldownCurrent < _enemySelf._attackCooldownMax)
            {
                ChargeAbility(_enemySelf);
                CirclePlayer(_enemySelf);
            }
            else
            {
                if (Vector3.Distance(_enemySelf.transform.position, _enemySelf._getPlayerTarget.transform.position) > 1.0f)
                    MoveToPlayer(_enemySelf);
                else
                {
                    _enemySelf.PerformAbility(_enemySelf._abilities[0]);
                    _enemySelf._attackCooldownCurrent = 0.0f;
                }
            }
        }

        static public void RangedAttackPlayer(EnemyBehaviour _enemySelf)
        {
            if (_enemySelf._attackCooldownCurrent < _enemySelf._attackCooldownMax)
            {
                ChargeAbility(_enemySelf);
                CirclePlayer(_enemySelf);
            }
            else
            {
                RangedEnemyBehaviour rangedEnemy = _enemySelf as RangedEnemyBehaviour;

                if (rangedEnemy._rangedAttackChargeUpCurrent < rangedEnemy._rangedAttackChargeUpMax)
                   rangedEnemy._rangedAttackChargeUpCurrent += Time.fixedDeltaTime;
                else
                {
                    rangedEnemy.PerformAbility(rangedEnemy._abilities[0]);
                    rangedEnemy._rangedAttackChargeUpCurrent = 0.0f;
                    rangedEnemy._attackCooldownCurrent = 0.0f;
                }
            }

            // else
            // {
            //     _enemySelf.PerformAbility(_enemySelf._abilities[0]);
            //     _enemySelf._attackCooldownCurrent = 0.0f;
            // }
        }

        static public void MoveToPlayer(EnemyBehaviour _enemySelf)
        {
            Vector3 enemySelfPosition = _enemySelf.transform.position;
            _enemySelf._moveVector = (_enemySelf._getPlayerTarget.transform.position - enemySelfPosition).normalized;
        }

        static public void RunFromPlayer(EnemyBehaviour _enemySelf)
        {
            Vector3 enemySelfPosition = _enemySelf.transform.position;
            _enemySelf._moveVector = -(_enemySelf._getPlayerTarget.transform.position - enemySelfPosition).normalized;
        }

        static private void CirclePlayer(EnemyBehaviour _enemySelf)
        {
            //References to make code readable
            Vector3 enemySelfPosition = _enemySelf.transform.position;
            Vector3 targetPosition = _enemySelf._getPlayerTarget.transform.position;

            //Find Perpendicular angle
            Vector3 vectorOffset = targetPosition - enemySelfPosition;
            Vector2 perpendicular = Vector2.Perpendicular(new Vector2(vectorOffset.x, vectorOffset.z));
            Vector3 perpendicularOffset = new Vector3(perpendicular.x, 0, perpendicular.y);
            Vector3 AdjustedOffset;

            //Determine Steering away or towards to keep in smooth circle
            float distanceFromMinSafeZoneToEnemy = Vector3.Distance(enemySelfPosition, targetPosition) - _enemySelf._safeZoneDistanceMin;
            float distanceFromMaxSafeZoneToEnemy = Vector3.Distance(enemySelfPosition, targetPosition) - _enemySelf._safeZoneDistanceMax;
            float modifier = Mathf.Clamp(distanceFromMinSafeZoneToEnemy + distanceFromMaxSafeZoneToEnemy, -1.0f, 1.0f);

            if (_enemySelf._moveClockwise)
            {
                AdjustedOffset = -perpendicularOffset + ((vectorOffset * 0.66f) * modifier);
            }
            else
            {
                AdjustedOffset = perpendicularOffset + ((vectorOffset * 0.66f) * modifier);
            }

            _enemySelf._moveVector = AdjustedOffset.normalized * 0.66f;

            Debug.DrawLine(enemySelfPosition, enemySelfPosition + vectorOffset.normalized * distanceFromMinSafeZoneToEnemy, Color.green);
            Debug.DrawLine(enemySelfPosition, enemySelfPosition + vectorOffset.normalized * distanceFromMaxSafeZoneToEnemy, Color.green);
            Debug.DrawLine(enemySelfPosition, enemySelfPosition + AdjustedOffset.normalized * 3.0f, Color.blue);
        }

        static private void ChargeAbility(EnemyBehaviour _enemySelf)
        {
            _enemySelf._attackCooldownCurrent += Time.fixedDeltaTime;
        }

        static public void DetermineBehaviour(EnemyBehaviour _enemySelf)
        {
            if (_enemySelf._getPlayerTarget == null)
            {
                _enemySelf._behaviour = EBehaviours.GoToHome;
                return;
            }

            if (_enemySelf._attackCooldownCurrent >= _enemySelf._attackCooldownMax)
            {
                _enemySelf._behaviour = EBehaviours.AttackPlayer;
                return;
            }

            Vector3 targetPosition = _enemySelf._getPlayerTarget.transform.position;
            Vector3 enemySelfPosition = _enemySelf.transform.position;

            if (Vector3.Distance(targetPosition, enemySelfPosition) > _enemySelf._safeZoneDistanceMax)
                _enemySelf._behaviour = EBehaviours.MoveToPlayer;

            else if (Vector3.Distance(targetPosition, enemySelfPosition) < _enemySelf._safeZoneDistanceMin)
                _enemySelf._behaviour = EBehaviours.RunFromPlayer;
            else
                _enemySelf._behaviour = EBehaviours.AttackPlayer; 
            
        }
}
