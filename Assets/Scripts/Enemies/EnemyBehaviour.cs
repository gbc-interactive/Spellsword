using Spellsword;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Spellsword
{
    public enum EBehaviours
    {
        Idle,
        GoToHome,
        FoundPlayer,
        MoveToPlayer,
        AttackPlayer,
        RunFromPlayer

    }

    public class EnemyBehaviour : CharacterBase, ITriggerCallbackable
    {
        private GameObject _playerTarget;
        public GameObject _getPlayerTarget
        {  get { return _playerTarget; } }

        [HideInInspector] public EBehaviours _behaviour;

        [HideInInspector] public Vector3 _moveVector;
        [HideInInspector] public Vector3 _homePosition;
        [HideInInspector] public bool _moveClockwise;

        [HideInInspector] public float _attackCooldownCurrent = 0.0f;


        [Header("References")]
        [SerializeField] private SphereCollider _sightMaximum;

        [Header("Stats")]
        [SerializeField] public float _attackCooldownMax;
        [SerializeField] public List<AbilityBase> _abilities = new List<AbilityBase>();

        [Header("Zones Distances")]
        [SerializeField] public float _safeZoneDistanceMax;
        [SerializeField] public float _safeZoneDistanceMin;


        private void Start()
        {
            _homePosition = transform.position;
        }

        public void TriggerEnterCallback(Collider other)
        {
            if (other.tag != "Player") return;

            _playerTarget = other.gameObject;
            _behaviour = EBehaviours.FoundPlayer;
        }


        public void TriggerExitCallback(Collider other)
        {
            if (other.tag == "Player")
                _playerTarget = null;
        }
    }

}