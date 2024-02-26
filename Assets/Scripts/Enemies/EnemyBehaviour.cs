using Spellsword;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class AbilityForAI
{
    public AbilityBase ability;

    [HideInInspector] public float cooldownCurrentCount;
    public float cooldownMaxCount;

    [HideInInspector] public float chargeUpCurrentCount;
    public float chargeUpMaxCount;
}

namespace Spellsword
{
    // public enum EBehaviours
    // {
    //     Idle,
    //     GoToHome,
    //     MoveToPlayer,
    //     AttackPlayer,
    //     RunFromPlayer
    // }

    public class EnemyBehaviour : CharacterBase, ITriggerCallbackable
    {
        private GameObject _playerTarget;
        public GameObject _getPlayerTarget {  get { return _playerTarget; } }

        private BaseAIBehaviour _behaviour;

        [HideInInspector] public NavMeshAgent _navAgent;
        [HideInInspector] public Vector3 _homePosition;
        [HideInInspector] public bool _moveClockwise;

        [Header("References")]
        [SerializeField] private SphereCollider _sightMaximum;

        [Header("Stats")]
        [Range(0.01f, 1.0f)] public float _circleSpeedScale;
        [SerializeField] public List<AbilityForAI> _abilities = new List<AbilityForAI>();

        [Header("Zones Distances")]
        [SerializeField] public float _safeZoneDistanceMax;
        [SerializeField] public float _safeZoneDistanceMin;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
        }
 
        protected virtual void Start()
        {
            _homePosition = transform.position;
            _behaviour = BehavioursAI.idle;
        }

        protected virtual void FixedUpdate()
        {
            ChargeCooldowns();
            DetermineBehaviour();
            RunBehaviour();
        }

        private void ChargeCooldowns()
        {
            foreach (AbilityForAI ability in _abilities)
            {
                if (ability.cooldownCurrentCount > ability.cooldownMaxCount)
                    return;

                ability.cooldownCurrentCount += Time.fixedDeltaTime;
            }
        }

        protected void RunBehaviour()
        {
            _behaviour.UpdateBehaviour(this);
            
        }

        protected void SwitchBehaviour(BaseAIBehaviour newBehaviour)
        {
            if (_behaviour == newBehaviour)
                return;

            if (_behaviour != null)
                _behaviour.ExitBehaviour(this);
            
            _behaviour = newBehaviour;
            _behaviour.EnterBehaviour(this);
        }

        protected virtual void DetermineBehaviour()
        {

        }

        public void TriggerEnterCallback(Collider other)
        {
            if (other.tag != "Player") return;
            _playerTarget = other.gameObject;
        }


        public void TriggerExitCallback(Collider other)
        {
            if (other.tag == "Player")
                _playerTarget = null;
        }

    }

}