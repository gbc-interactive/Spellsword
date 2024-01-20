using Spellsword;
using System.Collections;
using System.Collections.Generic;
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
                    break;

                case EBehaviours.AttackPlayer:
                    break;
            }

            TryMove(_moveVector);
        }

        private void GoToHome()
        {
            if (_homePosition != Vector3.zero || Vector3.Distance(_homePosition, transform.position) > 3.0f)
                _moveVector = (_homePosition - transform.position).normalized;
            else
                _behaviour = EBehaviours.Idle;
        }

        private void FoundPlayer()
        {
            if (_getPlayerTarget != null)
                _behaviour = EBehaviours.MoveToPlayer;
        }

        private void MoveToPlayer()
        {
            if (_getPlayerTarget != null)
                _moveVector = (_getPlayerTarget.transform.position - transform.position).normalized;
            else
                _behaviour = EBehaviours.GoToHome;
        }

    }
}