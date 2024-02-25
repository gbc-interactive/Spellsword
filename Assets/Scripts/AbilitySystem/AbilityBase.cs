using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spellsword
{
    public class AbilityBase : MonoBehaviour
    {
        public ParticleSystem _particleSystem;
        [SerializeField] private EAbilityType _abilityType;
        [Header("Ability Stats")]
        [SerializeField] protected float _cooldownTime;
        [SerializeField] protected int _damageValue;
        [SerializeField] public float _MPCost;
        [Header("Ability Management")]
        [SerializeField] protected float _lastCastTime;
        [SerializeField] public bool _isOnCooldown = false;        
        [SerializeField] public bool isCharging = false;
        [SerializeField] public float chargeTime = 0.0f;
        
        void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _lastCastTime = -_cooldownTime;
        }
        public enum EAbilityType 
        {
            OneHit,
            TwoHit,
            Default = OneHit
        }
        public enum Status
        {
            Gust
        }

        //ability collision
        private void OnTriggerEnter(Collider other)
        {
            HandleCollision(other);
        }

        public virtual void HandleCollision(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //ApplyDamage(ref HP, 10);
                Debug.Log("Collsion");
            }
            else if (other.CompareTag("Enemy"))
            {
                //ApplyDamage(ref HP, 10);
                Debug.Log("CollsionEnemy");
            }
        }

        void Update()
        {
            if (_isOnCooldown)
            {
                CooldownManagement();
            }
        }

        public void CooldownManagement()
        {
            if (Time.time - _lastCastTime >= _cooldownTime)
            {
                _isOnCooldown = false;//ready to use again
            }
        }

        public void Cast()
        {
            if (Time.time - _lastCastTime >= _cooldownTime)
            {
                _isOnCooldown = true;
                _lastCastTime = Time.time;
                chargeTime = 0f;                
            }
        }

        public void ApplyDamage(ref float hp,float howMuch)
        {
            hp -= howMuch;
        }

        public virtual void PerformAbility(CharacterBase character, bool isPlayer)
        {
            if(_particleSystem != null)
            _particleSystem.Play();
        }
    }

}
