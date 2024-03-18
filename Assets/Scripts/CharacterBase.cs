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

    public enum EAnimationState
    {
        Idle,
        Run,
        Melee,
        Hurt
    }

    public abstract class CharacterBase : MonoBehaviour
    {
        public List<StatusEffect> statusEffects = new List<StatusEffect>();

        const string IDLE = "Idle";
        const string RUN = "Run";
        const string MELEE = "Melee";
        const string HURT = "Hurt";
        public EAnimationState characterState = EAnimationState.Idle;
        private bool isPlayingAnimation = false;

        [Header("References")]
        //[SerializeField] protected Animator m_animator = null;
        [SerializeField] private Rigidbody _rigidbody = null;
        [SerializeField] private SpriteRenderer _spriteRenderer = null;
        [SerializeField] private Animator _animator = null;

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

        public Material flashMaterial; // Material with the white flash shader
        private float flashDuration = 0.25f; // Duration of the flash
        private Material defaultMaterial; // Original material of the sprite

        public GameObject hat;


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
                    SetFacingDirection(EDirection.Left);
                }
                else if (vector.x < 0)
                {
                    SetFacingDirection(EDirection.Right);
                }

                characterState = EAnimationState.Run;
                return true;
            }
            else
            {
                // Can't move if there's no direction to move in
                characterState = EAnimationState.Idle;
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
                if(ability.PerformAbility(this, isPlayer))
                {
                    _currentMP -= ability._MPCost;
                }
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
            if (characterState == EAnimationState.Hurt) return false;
            Miscast();
            Debug.Log("taking damage" + damage);
            characterState = EAnimationState.Hurt;
            Flash();
            SetAnimation();
            _timeSinceLastHit = 0;
            _currentHP -= damage;
            if(_currentHP <= 0)
            {
                Die();
            }
            return true;
        }

        public void Flash()
        {
            defaultMaterial = _spriteRenderer.material; //TODO: move to Start/Awake later
            _spriteRenderer.material = flashMaterial;
            Invoke(nameof(ResetFlash), 0.25f);
        }

        public void ResetFlash()
        {
            _spriteRenderer.material = defaultMaterial;
        }

        private IEnumerator FlashCoroutine()
        {

            yield return new WaitForSeconds(flashDuration);

        }

        public virtual void Die()
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            hat.GetComponent<SpriteRenderer>().flipX = gameObject.GetComponent<SpriteRenderer>().flipX;
            hat.SetActive(true);
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

        public void SetAnimation()
        {
            if (_animator == null) return;

            if (!isPlayingAnimation)
            {
                switch (characterState)
                {
                    case EAnimationState.Idle:
                        _animator.Play(IDLE);
                        break;
                    case EAnimationState.Run:
                        _animator.Play(RUN);
                        break;
                    case EAnimationState.Melee:
                        _animator.Play(MELEE);
                        isPlayingAnimation = true;
                        Invoke(nameof(ResetAnimation), 0.5f);
                        break;
                    case EAnimationState.Hurt:
                        _animator.Play(HURT);
                        isPlayingAnimation = true;
                        Invoke(nameof(ResetAnimation), 0.5f);
                        break;
                }
            }
        }

        private void ResetAnimation()
        {
            isPlayingAnimation = false;
            characterState = EAnimationState.Idle;
        }
        private void OnCollisionEnter(Collision other)
        {
            HandleCollision(other);
        }
        public virtual void HandleCollision(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //Miscast(false);
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                //Miscast(true);
            }
        }
        public virtual void Miscast() { }
    }
}
