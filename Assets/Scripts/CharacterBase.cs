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
        UseAbility = 4,
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

        [Header("Ability System")]
        [SerializeField] private float _maxMP = 100f;
        [SerializeField] private float _currentMP;
        [SerializeField] private Transform _castpoint;
        [SerializeField] private float _timeBetweenCast = 0.25f;

        private bool isUsingAbility = false;

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
                return _spriteRenderer.flipX ? EDirection.Left : EDirection.Left;
            }

            return EDirection.Default;
        }

        public bool PerformAbility(AbilityBase ability)
        {
            ability.PerformAbility();
            return true;
        }
    }
}
