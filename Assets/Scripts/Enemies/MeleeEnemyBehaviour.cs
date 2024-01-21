using Spellsword;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions.Must;


namespace Spellsword
{

    public class MeleeEnemyBehaviour : EnemyBehaviour
    {
        private void Start()
        {
            _behaviour = EBehaviours.Idle;
        }

        private void FixedUpdate()
        {
            _moveVector = Vector3.zero;

            switch( _behaviour )
            {
                case EBehaviours.Idle:
                    break;

                case EBehaviours.GoToHome:
                    GoToHome();
                    break;

                case EBehaviours.Patrol:
                    break;

                case EBehaviours.FoundPlayer:
                    FoundPlayer();
                    break;

                case EBehaviours.MoveToPlayer:
                    MoveToPlayer();
                    break;

                case EBehaviours.CirclePlayer:
                    CirclePlayer();
                    break;

                case EBehaviours.AttackPlayer:
                    break;

                case EBehaviours.RunFromPlayer:
                    RunFromPlayer();
                    break;
            }

            _moveVector = new Vector3(_moveVector.x, 0, _moveVector.z);
            TryMove(_moveVector);
        }

        private void GoToHome()
        {
            if (_homePosition != Vector3.zero || Vector3.Distance(_homePosition, transform.position) > 3.0f)
            {
                _moveVector = (_homePosition - transform.position).normalized;
            }
            else
            {
                _behaviour = EBehaviours.Idle;
            }
        }

        private void FoundPlayer()
        {
            if (_getPlayerTarget != null)
            {
                _behaviour = EBehaviours.MoveToPlayer;
            }
        }

        private void MoveToPlayer()
        {
            if (_getPlayerTarget == null) // early return if we can't see the player
            {
                _behaviour = EBehaviours.GoToHome;
                return;
            }

            if (Vector3.Distance(_getPlayerTarget.transform.position, transform.position) > _safeZoneDistanceMax - 0.5f)
            {
                _moveVector = (_getPlayerTarget.transform.position - transform.position).normalized;
            }
            else if (Vector3.Distance(_getPlayerTarget.transform.position, transform.position) < _safeZoneDistanceMin)
            {
                _behaviour = EBehaviours.RunFromPlayer;
            }
            else
            {
                _behaviour = EBehaviours.CirclePlayer;
            }

                
            
        }

        private void CirclePlayer()
        {
            if (_getPlayerTarget == null) // early return if we can't see the player
            {
                _behaviour = EBehaviours.GoToHome;
                return;
            }


            if (Vector3.Distance(_getPlayerTarget.transform.position, transform.position) > _safeZoneDistanceMax)
            {
                _behaviour = EBehaviours.MoveToPlayer;
            }
            else if (Vector3.Distance(_getPlayerTarget.transform.position, transform.position) < _safeZoneDistanceMin)
            {
                _behaviour = EBehaviours.RunFromPlayer;
            }
            else
            {
                Vector3 vectorOffset = _getPlayerTarget.transform.position - transform.position;

                Vector2 perpendicular = Vector2.Perpendicular(new Vector2(vectorOffset.x, vectorOffset.z));
                Vector3 perpendicularOffset = new Vector3(perpendicular.x, 0, perpendicular.y);

                if (_moveClockwise)
                    _moveVector = -perpendicularOffset.normalized * 0.5f;
                else
                    _moveVector = perpendicularOffset.normalized * 0.66f;



                Debug.DrawLine(_getPlayerTarget.transform.position, _getPlayerTarget.transform.position - vectorOffset.normalized * 6.0f, Color.red);
                Debug.DrawLine(transform.position, transform.position - perpendicularOffset.normalized * 3.0f, Color.green);
            }
        }

        private void RunFromPlayer()
        {
            if (_getPlayerTarget == null) // early return if we can't see the player
            {
                _behaviour = EBehaviours.GoToHome;
                return;
            }


            if (Vector3.Distance(_getPlayerTarget.transform.position, transform.position) > _safeZoneDistanceMax)
            {
                _behaviour = EBehaviours.MoveToPlayer;
            }
            else if (Vector3.Distance(_getPlayerTarget.transform.position, transform.position) < _safeZoneDistanceMin - 0.5f)
            {
                _moveVector = -(_getPlayerTarget.transform.position - transform.position).normalized;
            }
            else
            {
                _behaviour = EBehaviours.CirclePlayer;
            }
           

        }

    }


}