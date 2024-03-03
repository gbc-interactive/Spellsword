using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    [Flags]
    public enum EActionFlags
    {
        None = 0,
        Move = 1,
        Interact = 2,
        UseAbility = 3,
        All = ~0
    }

    public enum EDirection
    {
        Left,
        Right,
        Default = Right
    }

    public abstract class CharacterBase : MonoBehaviour
    {
        public List<StatusEffect> statusEffects = new List<StatusEffect>();

        [Header("References")]
        //[SerializeField] protected Animator m_animator = null;
        [SerializeField] private Rigidbody _rigidbody = null;
        [SerializeField] private SpriteRenderer _spriteRenderer = null;

        [Header("Movement")]
        [SerializeField] public float _moveSpeed = 5.0f;

        public float _maxHP = 100f;
        public float _maxMP = 100f;
        public float _currentHP;
        public float _currentMP;

        [Header("Ability System")]
        [SerializeField] private Transform _castpoint;
        [SerializeField] private float _timeBetweenCast = 0.25f;
        [SerializeField] private float _regenIntervalMP = 5.0f;
        [SerializeField] private float _regenRateMP = 10.0f;
        public float _timeSinceLastAbility = 0.0f;

        [SerializeField] private float _regenIntervalHP = 5.0f;
        [SerializeField] private float _regenRateHP = 10.0f;
        private float _timeSinceLastHit = 0.0f;

        private bool isUsingAbility = false;

        private void Update()
        {
            UpdateStatusEffects();

            if (_currentHP > 0)
            {
                //Regen HP
                _timeSinceLastHit += Time.deltaTime;

                if (_timeSinceLastHit >= _regenIntervalHP)
                {
                    RegenHP();
                }

                //Regen MP
                _timeSinceLastAbility += Time.deltaTime;

                if (_timeSinceLastAbility >= _regenIntervalMP)
                {
                    RegenMP();
                }
            }
        }

        public bool TryMove(Vector3 vector)
        {
            if (vector != Vector3.zero)
            {
                // Multiply the movement vector by Time.deltaTime
                vector *= Time.deltaTime;

                _rigidbody.MovePosition(_rigidbody.position + vector * _moveSpeed);

                if (vector.x > 0)
                {
                    SetFacingDirection(EDirection.Right);
                }
                else if (vector.x < 0)
                {
                    SetFacingDirection(EDirection.Left);
                }

                return true;
            }
            else
            {
                // Can't move if there's no direction to move in
                return false;
            }
        }

        public void SetFacingDirection(EDirection direction)
        {
            if (_spriteRenderer)
            {
                _spriteRenderer.flipX = (direction == EDirection.Left);
            }
        }

        public EDirection GetFacingDirection()
        {
            if (_spriteRenderer)
            {
                return _spriteRenderer.flipX ? EDirection.Left : EDirection.Right;
            }

            return EDirection.Default;
        }

        public virtual bool PerformAbility(AbilityBase ability, bool isPlayer)
        {
            if (!ability.gameObject.activeInHierarchy)
                return false;

            if (_currentMP >= ability._MPCost && !ability._isOnCooldown)
            {
                //_timeSinceLastAbility = 0;
                _currentMP -= ability._MPCost;
                ability.PerformAbility(this, isPlayer);
                return true;

            }
            else
            {
                Debug.Log("not enough mana");
                return false;
            }
        }

        public virtual bool TakeDamage(int damage)
        {
            Debug.Log("taking damage" + damage);
            _timeSinceLastHit = 0;
            _currentHP -= damage;
            if(_currentHP <= 0)
            {
                Die();
            }
            return true;
        }

        public virtual void Die()
        {
            gameObject.SetActive(false);
        }

        public virtual void RegenMP()
        {
            _currentMP = Mathf.Clamp(_currentMP + (_regenRateMP * Time.deltaTime), 0f, _maxMP);
        }

        public virtual void RegenHP()
        {
            _currentHP = Mathf.Clamp(_currentHP + (_regenRateHP * Time.deltaTime), 0f, _maxHP);
        }

        public virtual void UpdateStatusEffects()
        {
            for (int i = 0; i < statusEffects.Count; i++)
            {
                if (statusEffects[i] != null)
                {
                    statusEffects[i].UpdateEffect();
                }
            }
        }
    }
}
