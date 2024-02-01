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
        [Header("References")]
        //[SerializeField] protected Animator m_animator = null;
        [SerializeField] private Rigidbody _rigidbody = null;
        [SerializeField] private SpriteRenderer _spriteRenderer = null;

        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 5.0f;

        public float _maxHP = 100f;
        public float _maxMP = 100f;
        public float _currentHP;
        public float _currentMP;

        [Header("Ability System")]
        [SerializeField] private Transform _castpoint;
        [SerializeField] private float _timeBetweenCast = 0.25f;
        [SerializeField] private float _regenIntervalMP = 5.0f;
        [SerializeField] private float _regenRateMP = 10.0f;
        private float _timeSinceLastAbility = 0.0f;

        private bool isUsingAbility = false;

        private void Update()
        {
            //Regen MP
            _timeSinceLastAbility += Time.deltaTime;

            if (_timeSinceLastAbility >= _regenIntervalMP)
            {
                RegenMP();
            }
        }

        public bool TryMove(Vector3 vector)
        {
            vector = new Vector3(vector.x, vector.y, vector.z * 2.0f); //vertical speed compensation

            if (vector != Vector3.zero)
            {
                _rigidbody.MovePosition(_rigidbody.position + vector * _moveSpeed * Time.fixedDeltaTime);
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

        public virtual bool PerformAbility(AbilityBase ability)
        {
            ability.PerformAbility();
            return true;
            //if (_currentMP >= ability._manaCost)
            //{
            //      _currentMP -= ability._manaCost;
            //      ability.PerformAbility();
            //      return true;
            //    

            //}
            //else
            //{
            //    Debug.Log("not enough mana");
            //    return false;
            //}
        }
            if(_currentMP >= ability.MPCost)
            {
                _timeSinceLastAbility = 0;
                _currentMP -= ability.MPCost;
                ability.PerformAbility();
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void RegenMP()
        {
            _currentMP = Mathf.Clamp(_currentMP + (_regenRateMP * Time.deltaTime), 0f, _maxMP);
        }

        public void RegenHP()
        {

        }
    }
}
