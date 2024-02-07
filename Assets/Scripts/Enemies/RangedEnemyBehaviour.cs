using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class RangedEnemyBehaviour : EnemyBehaviour
    {
        [HideInInspector] public float _rangedAttackChargeUpCurrent;
        
        [Header("Ranged Charge Up Time")]
        [SerializeField] public float _rangedAttackChargeUpMax;

        
        void FixedUpdate()
        {
            _moveVector = Vector3.zero;

            if (_rangedAttackChargeUpCurrent == 0.0f)
                BehavioursAI.DetermineBehaviour(this);

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
                    BehavioursAI.RangedAttackPlayer(this);
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