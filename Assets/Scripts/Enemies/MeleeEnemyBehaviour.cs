using Spellsword;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


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

            switch(_behaviour)
            {
                case EBehaviours.Idle:
                    break;

                case EBehaviours.GoToHome:
                    BehavioursAI.GoToHome(this);
                    break;

                case EBehaviours.Patrol:
                    break;

                case EBehaviours.FoundPlayer:
                    BehavioursAI.FoundPlayer(this);
                    break;

                case EBehaviours.MoveToPlayer:
                    BehavioursAI.MoveToPlayer(this);
                    break;

                case EBehaviours.CirclePlayer:
                    BehavioursAI.CirclePlayer(this);
                    break;

                case EBehaviours.AttackPlayer:
                    break;

                case EBehaviours.RunFromPlayer:
                    BehavioursAI.RunFromPlayer(this);
                    break;
            }

            _moveVector = new Vector3(_moveVector.x, 0, _moveVector.z);
            TryMove(_moveVector);
        }

    }


}