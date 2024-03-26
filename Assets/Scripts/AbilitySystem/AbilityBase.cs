using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spellsword
{
    public class AbilityBase : MonoBehaviour
    {
        public ParticleSystem _particleSystem;
        [SerializeField] private EAbilityType _abilityType;
        [SerializeField] protected float _cooldownTime;
        [SerializeField] protected float _lastCastTime;
        [SerializeField] public float _MPCost;        
        [SerializeField] public bool _isOnCooldown;
        [SerializeField] public float _chargeTime;
        [SerializeField] public bool _isCharging;
        void Start()
        {
            _chargeTime = 0.0f;
            _isCharging = false;
            _isOnCooldown = false;
            _particleSystem = GetComponent<ParticleSystem>();
            _lastCastTime = -_cooldownTime;
        }
        public enum EAbilityType 
        {
            Attack1,
            Attack2,
            Default = Attack1
        }
       

        public virtual void Update()
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
                _chargeTime = 0f;                
            }
        }

        public virtual void StartCharging()
        {
            if (_isCharging) return;
            _isCharging = true;
        }

        public virtual bool PerformAbility(CharacterBase character, bool isPlayer)
        {
            if(_particleSystem != null)
            _particleSystem.Play();
            return true;
        }
    }

}
