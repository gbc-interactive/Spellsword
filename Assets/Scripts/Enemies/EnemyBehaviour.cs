using Spellsword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{

    public class EnemyBehaviour : CharacterBase, ITriggerCallbackable
    {
        private GameObject _playerTarget;

        [Header("References")]
        [SerializeField] private SphereCollider _sightMaximum;

        [Header("Zones Distances")]
        [SerializeField] private float _safeZoneDistanceMax;
        [SerializeField] private float _safeZoneDistanceMin;


        public void TriggerEnterCallback(Collider other)
        {
            if (other.tag == "Player")
                _playerTarget = other.gameObject;
        }

        public void TriggerExitCallback(Collider other)
        {
            if (other.tag == "Player")
                _playerTarget = null;
        }
    }

}