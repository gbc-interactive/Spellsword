using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

static public class BehavioursAI
{
        static public void Idle(EnemyBehaviour _enemySelf)
        {
            _enemySelf._moveVector = Vector3.zero;
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
            //DetermineCircleDirection(_enemySelf);

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
                AdjustedOffset = -perpendicularOffset + (vectorOffset * _enemySelf._circleSpeed * modifier);
            }
            else
            {
                AdjustedOffset = perpendicularOffset + (vectorOffset * _enemySelf._circleSpeed * modifier);
            }

            _enemySelf._moveVector = AdjustedOffset.normalized * 0.66f;

            //TODO Remove Debug Drawings
            Debug.DrawLine(enemySelfPosition, enemySelfPosition + vectorOffset.normalized * distanceFromMinSafeZoneToEnemy, Color.green);
            Debug.DrawLine(enemySelfPosition, enemySelfPosition + vectorOffset.normalized * distanceFromMaxSafeZoneToEnemy, Color.green);
            Debug.DrawLine(enemySelfPosition, enemySelfPosition + AdjustedOffset.normalized * 3.0f, Color.blue);
        }

        static private void ChargeAbility(EnemyBehaviour _enemySelf)
        {
            _enemySelf._attackCooldownCurrent += Time.fixedDeltaTime;
        }

        static private void DetermineCircleDirection(EnemyBehaviour _enemySelf)
        {
            if (_enemySelf._moveVector.x <= 0.0f)
            {
                if (_enemySelf._moveVector.y < 0.0f)
                {
                    _enemySelf._moveClockwise = false;
                }
                else
                {
                    _enemySelf._moveClockwise = true;
                }
            }
            else
            {
                if (_enemySelf._moveVector.y < 0.0f)
                {
                    _enemySelf._moveClockwise = true;
                }
                else
                {
                    _enemySelf._moveClockwise = false;
                }
            }
        }

#region Melee Specific Enemy AI
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
#endregion
#region Ranged Specific Enemy AI
        static public void RangedAttackPlayer(EnemyBehaviour _enemySelf)
        {
            if (_enemySelf._attackCooldownCurrent < _enemySelf._attackCooldownMax)
            {
                ChargeAbility(_enemySelf);
                CirclePlayer(_enemySelf);
            }
            else
            {
                _enemySelf._moveVector = Vector3.zero;
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
        }
#endregion
#region Magic Specific Enemy AI
        static public void MagicFireballAbility(EnemyBehaviour _enemySelf)
        {
            if (_enemySelf._attackCooldownCurrent < _enemySelf._attackCooldownMax)
            {
                ChargeAbility(_enemySelf);
                CirclePlayer(_enemySelf);
            }
            else
            {
                _enemySelf._moveVector = Vector3.zero;
                MagicEnemyBehaviour magicEnemy = _enemySelf as MagicEnemyBehaviour;

                if (magicEnemy._magicFireballChargeUpCurrent < magicEnemy._magicFireballChargeUpMax)
                {
                    magicEnemy._magicFireballChargeUpCurrent += Time.fixedDeltaTime;
                }
                else
                {
                    magicEnemy.PerformAbility(_enemySelf._abilities[0]);
                    magicEnemy._magicFireballChargeUpCurrent = 0.0f;
                    magicEnemy._attackCooldownCurrent = 0.0f;
                }
            }
        }

        static public void MagicRunFromPlayer(EnemyBehaviour _enemySelf)
        {
            MagicEnemyBehaviour magicEnemy = _enemySelf as MagicEnemyBehaviour;

            if (magicEnemy._magicBlinkCoolDownCurrent < magicEnemy._magicBlinkCoolDownMax)
            {
                RunFromPlayer(_enemySelf);
                return;
            }

            //FIXME: Unspaghettify this
            RaycastHit hit;

            Vector3 playerDirection = (_enemySelf.transform.position - _enemySelf._getPlayerTarget.transform.position).normalized;
            Vector3 blinkDirectionSpot = new Vector3(playerDirection.x, -0.125f, playerDirection.z);

            if (Physics.Raycast(_enemySelf.transform.position, blinkDirectionSpot, out hit, 7.0f))
            {
                if (hit.collider.gameObject.name != "Ground")
                {
                    RunFromPlayer(_enemySelf);
                }
                else
                {
                    magicEnemy.PerformAbility(_enemySelf._abilities[1]);
                    magicEnemy.transform.position = new Vector3(hit.point.x, magicEnemy.transform.position.y, hit.point.z);
                    magicEnemy._magicBlinkCoolDownCurrent = 0.0f;
                }
            }
        }
#endregion

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
