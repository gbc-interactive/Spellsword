using System;
using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;
using UnityEngine.AI;

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
            _enemySelf._behaviour = EBehaviours.MoveToPlayer;

    }

    static public void GoToHome(EnemyBehaviour _enemySelf)
    {
        bool doesEnemyHaveHome = _enemySelf._homePosition != null;
        bool isEnemyAtHome = Vector3.Distance(_enemySelf.transform.position, _enemySelf._homePosition) < 3.0f;

        if (doesEnemyHaveHome && !isEnemyAtHome)
            _enemySelf._navAgent.SetDestination(_enemySelf._homePosition);
        else
            _enemySelf._behaviour = EBehaviours.Idle;

    }
 
    static public void MoveToPlayer(EnemyBehaviour _enemySelf)
    {
        Vector3 playerPosition = _enemySelf._getPlayerTarget.transform.position;
        _enemySelf._navAgent.SetDestination(playerPosition);
    }

    static public void RunFromPlayer(EnemyBehaviour _enemySelf)
    {
        Vector3 desiredSpace;
        float distance = 4.0f;

        Vector3 playerPosition = _enemySelf._getPlayerTarget.transform.position;
        Vector3 awayFromPlayerDirection = (_enemySelf.transform.position - playerPosition).normalized;

        //Check if away from player direction is valid
        NavMeshHit hit;
        desiredSpace = _enemySelf.transform.position + awayFromPlayerDirection * distance;
        
        bool isDestinationBlocked = _enemySelf._navAgent.Raycast(desiredSpace, out hit);

        if (!isDestinationBlocked)
        {
            _enemySelf._navAgent.SetDestination(desiredSpace);
            return;
        }

        CirclePlayer(_enemySelf);
    }

    static private void CirclePlayer(EnemyBehaviour _enemySelf)
    {
        Vector3 enemySelfPosition = _enemySelf.transform.position;
        Vector3 playerPosition = _enemySelf._getPlayerTarget.transform.position;

        // Find angle/direction to move in
        Vector3 playerDirectionVector = playerPosition - enemySelfPosition;
        Vector2 perpendicularPlayer2D = Vector2.Perpendicular(new Vector2(playerDirectionVector.x, playerDirectionVector.z));
        Vector3 perpendicularPlayer = new Vector3(perpendicularPlayer2D.x, 0, perpendicularPlayer2D.y);

        // Determine Steering away or towards to keep in smooth circle
        float distanceToPlayer = Vector3.Distance(enemySelfPosition, playerPosition);
        float distanceFromMinSafeZone = distanceToPlayer - _enemySelf._safeZoneDistanceMin;
        float distanceFromMaxSafeZone = distanceToPlayer - _enemySelf._safeZoneDistanceMax;
        float distanceFromMidpoint = Mathf.Clamp(distanceFromMinSafeZone + distanceFromMaxSafeZone, -1.0f, 1.0f);

        Vector3 correctedPath = playerDirectionVector * _enemySelf._circleSpeedScale * distanceFromMidpoint;

        if (_enemySelf._moveClockwise)
            correctedPath -= perpendicularPlayer;
        else
            correctedPath += perpendicularPlayer;

        Vector3 desiredDestination = enemySelfPosition + correctedPath.normalized * 1.5f;
        _enemySelf._navAgent.SetDestination(desiredDestination);

        NavMeshHit hit;
        if (_enemySelf._navAgent.Raycast(desiredDestination, out hit))
            _enemySelf._moveClockwise = !_enemySelf._moveClockwise;
    }

    static private void ChargeAbility(EnemyBehaviour _enemySelf)
    {
        _enemySelf._attackCooldownCurrent += Time.fixedDeltaTime;
    }

    static private void DetermineCircleDirection(EnemyBehaviour _enemySelf)
    {
        Vector3 enemyPosition = _enemySelf.transform.position;
        Vector3 playerPosition = _enemySelf._getPlayerTarget.transform.position;
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

        if (Physics.Raycast(_enemySelf.transform.position, blinkDirectionSpot, out hit, 10.0f))
        {
            if (hit.collider.gameObject.name != "GroundMesh")
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
