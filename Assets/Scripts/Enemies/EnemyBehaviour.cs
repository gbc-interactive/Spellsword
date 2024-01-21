using Spellsword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public enum EBehaviours
    {
        Idle,
        GoToHome,
        Patrol,
        FoundPlayer,
        MoveToPlayer,
        CirclePlayer,
        AttackPlayer,
        RunFromPlayer
    }


    public class EnemyBehaviour : CharacterBase, ITriggerCallbackable
    {
        private GameObject _playerTarget;
        protected GameObject _getPlayerTarget
        {  get { return _playerTarget; } }

        protected EBehaviours _behaviour;

        protected Vector3 _moveVector;
        protected Vector3 _homePosition;

        protected bool _moveClockwise;

        [Header("References")]
        [SerializeField] private SphereCollider _sightMaximum;

        [Header("Zones Distances")]
        [SerializeField] protected float _safeZoneDistanceMax;
        [SerializeField] protected float _safeZoneDistanceMin;


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