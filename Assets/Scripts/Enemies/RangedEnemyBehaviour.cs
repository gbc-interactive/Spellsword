using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class RangedEnemyBehaviour : EnemyBehaviour
    {
        
        void Start()
        {
            _behaviour = EBehaviours.Idle;
        }

        
        void FixedUpdate()
        {
            _moveVector = Vector3.zero;

            switch(_behaviour)
            {
                case EBehaviours.Idle:
                    BehavioursAI.Idle(this);
                    break;

                case EBehaviours.GoToHome:
                    BehavioursAI.GoToHome(this);
                    break;

                case EBehaviours.FoundPlayer:
                    BehavioursAI.FoundPlayer(this);
                    break;

                case EBehaviours.MoveToPlayer:
                    BehavioursAI.MoveToPlayer(this);
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