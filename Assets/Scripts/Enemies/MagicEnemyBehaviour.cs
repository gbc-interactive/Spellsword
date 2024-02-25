using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class MagicEnemyBehaviour : EnemyBehaviour
    {

        [Header("Fireball Spell")]
        [SerializeField] public float _magicFireballChargeUpMax;
        [HideInInspector] public float _magicFireballChargeUpCurrent;

        [Header("Blink Spell")]
        [SerializeField] public float _magicBlinkCoolDownMax;
        [HideInInspector] public float _magicBlinkCoolDownCurrent;

        void Update()
        {
            // if (_getPlayerTarget == null)
            //     return;

            //RaycastHit hit;

            //Vector3 playerDirection = (transform.position - _getPlayerTarget.transform.position).normalized;

            //playerDirection = new Vector3(playerDirection.x, -0.125f, playerDirection.z);

            //DEBUG: Teleport Ray Checker lines
            // if (Physics.Raycast(transform.position, playerDirection, out hit, 10.0f))
            // {
            //     Debug.DrawRay(transform.position, playerDirection * 10.0f, Color.yellow);
            //     Debug.DrawRay(transform.position, playerDirection * hit.distance, Color.red);
            // }

        }

        void FixedUpdate()
        {
            if (_magicBlinkCoolDownCurrent < _magicBlinkCoolDownMax)
            {
                _magicBlinkCoolDownCurrent += Time.fixedDeltaTime;
            }

            if (_magicFireballChargeUpCurrent == 0.0f)
                BehavioursAI.DetermineBehaviour(this);

            switch (_behaviour)
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

                    BehavioursAI.MagicFireballAbility(this);

                    break;

                case EBehaviours.RunFromPlayer:
                    BehavioursAI.MagicRunFromPlayer(this);

                    break;
            }

            // _moveVector = new Vector3(_moveVector.x, 0, _moveVector.z);
            // TryMove(_moveVector);
        }
    }
}
