using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class MagicEnemyBehaviour : EnemyBehaviour
    {
        [HideInInspector] public float _magicBlinkCoolDownCurrent;
        
        [Header("Blink Cool Down Time")]
        [SerializeField] public float _magicBlinkCoolDownMax;

        void FixedUpdate()
        {
            _moveVector = Vector3.zero;

            if (_magicBlinkCoolDownCurrent < _magicBlinkCoolDownMax)
            {
                _magicBlinkCoolDownCurrent += Time.fixedDeltaTime;
            }
            
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

                    BehavioursAI.MagicAttackPlayer(this);

                    break;

                case EBehaviours.RunFromPlayer:

                    if (_magicBlinkCoolDownCurrent >= _magicBlinkCoolDownMax)
                        BehavioursAI.MagicBlinkAbility(this);

                    else
                        BehavioursAI.RunFromPlayer(this);

                    break;
            }

            _moveVector = new Vector3(_moveVector.x, 0, _moveVector.z);
            TryMove(_moveVector);
        }
    }
}
