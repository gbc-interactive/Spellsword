using System.Collections;
using System.Collections.Generic;
using Spellsword;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CirclePlayerBehaviour : BaseAIBehaviour 
{
    public override void EnterBehaviour(EnemyBehaviour _enemySelf)
    {
        if (Random.Range(1, 3) % 2 == 0)
            _enemySelf._moveClockwise = true;
        else
            _enemySelf._moveClockwise = false;
    }

    public override void UpdateBehaviour(EnemyBehaviour _enemySelf)
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

}
