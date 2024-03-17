using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Spellsword
{
    public class PlayerController : CharacterBase
    {
        private bool _isInitialized = false;

        private Vector3 _moveVector = Vector3.zero;

        public float damageStat = 10.0f;


        [SerializeField] private List<AbilityBase> _abilities = new List<AbilityBase>();
        public Vector3 mousePosition;
        public Vector3 worldPosition;

        public void Initialize()
        {
            GameManager._inputActions.Player.Move.performed += OnInputMovePerformed;
            GameManager._inputActions.Player.Move.canceled += OnInputMoveCanceled;
            GameManager._inputActions.Player.Interact.performed += OnInputInteractPerformed;
            GameManager._inputActions.Player.Melee.performed += OnInputMeleePerformed;
            GameManager._inputActions.Player.Teleport.performed += OnInputTeleportPerformed;
            GameManager._inputActions.Player.Teleport.canceled += OnInputTeleportCanceled;
            GameManager._inputActions.Player.Gust.performed += OnInputGustPerformed;
            GameManager._inputActions.Player.FrostTrap.performed += OnInputFrostTrapPerformed;
            GameManager._inputActions.Player.FireBall.started += OnInputFireBallPerformed;
            GameManager._inputActions.Player.FireBall.canceled += OnInputFireBallCanceled;
            _isInitialized = true;
        }

        private void OnDisable()
        {
            GameManager._inputActions.Player.Move.performed -= OnInputMovePerformed;
            GameManager._inputActions.Player.Move.canceled -= OnInputMoveCanceled;
            GameManager._inputActions.Player.Interact.performed -= OnInputInteractPerformed;
            GameManager._inputActions.Player.Melee.performed -= OnInputMeleePerformed;
            GameManager._inputActions.Player.Teleport.performed -= OnInputTeleportPerformed;
            GameManager._inputActions.Player.Gust.performed -= OnInputGustPerformed;
            GameManager._inputActions.Player.FrostTrap.performed -= OnInputFrostTrapPerformed;
            GameManager._inputActions.Player.FireBall.started -= OnInputFireBallPerformed;
            GameManager._inputActions.Player.FireBall.canceled -= OnInputFireBallCanceled;
            _isInitialized = false;
        }

        private void FixedUpdate()
        { 
            TryMove(_moveVector);
            SetAnimation();
        }

        private void OnInputMovePerformed(InputAction.CallbackContext value)
        {
            Vector2 moveVector2D = value.ReadValue<Vector2>();
            _moveVector = new Vector3(moveVector2D.x, 0, moveVector2D.y);
        }

        private void OnInputMoveCanceled(InputAction.CallbackContext value)
        {
            _moveVector = Vector3.zero;
        }

        private void OnInputInteractPerformed(InputAction.CallbackContext value)
        {
            GameManager.Instance._interactionSystem.isInteracting = true;
        }

        private void OnInputMeleePerformed(InputAction.CallbackContext value)
        {
            PerformAbility(_abilities[0], true);
        }

        private void OnInputTeleportPerformed(InputAction.CallbackContext value)
        {
            Time.timeScale = 0.5f;
        }
        private void OnInputTeleportCanceled(InputAction.CallbackContext value)
        {
            PerformAbility(_abilities[1], true);
            Time.timeScale = 1f;
        }
        private void OnInputGustPerformed(InputAction.CallbackContext value)
        {
            PerformAbility(_abilities[2], true);
        }
        private void OnInputFrostTrapPerformed(InputAction.CallbackContext value)
        {
            PerformAbility(_abilities[3], true);
        }
        private void OnInputFireBallPerformed(InputAction.CallbackContext value)
        {
            _abilities[4].isCharging = true;
            GameManager.Instance._playerController._moveSpeed /= 2;         
        }
        private void OnInputFireBallCanceled(InputAction.CallbackContext value)
        {
            _abilities[4].isCharging = false;
            GameManager.Instance._playerController._moveSpeed *= 2;
            PerformAbility(_abilities[4], true);
        }
        public override bool PerformAbility(AbilityBase ability, bool isPlayer)
        {
            base.PerformAbility(ability, isPlayer);
            UIManager.Instance._headsOverDisplay.SetCurrentMP(_currentMP);
            return true;
        }

        public override bool TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            UIManager.Instance._headsOverDisplay.SetCurrentHP(_currentHP);
            return true;
        }

        public override void Die()
        {
            base.Die();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public override void RegenMP()
        {
            base.RegenMP();
            UIManager.Instance._headsOverDisplay.SetCurrentMP(_currentMP);
        }

        public override void RegenHP()
        {
            base.RegenHP();
            UIManager.Instance._headsOverDisplay.SetCurrentHP(_currentHP);
        }
    }
}
