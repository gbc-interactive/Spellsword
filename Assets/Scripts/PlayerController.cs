using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Spellsword
{
    public class PlayerController : CharacterBase
    {
        private bool _isInitialized = false;

        private Vector3 _moveVector = Vector3.zero;

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
            _isInitialized = true;
        }

        private void OnDisable()
        {
            GameManager._inputActions.Player.Move.performed -= OnInputMovePerformed;
            GameManager._inputActions.Player.Move.canceled -= OnInputMoveCanceled;
            GameManager._inputActions.Player.Interact.performed -= OnInputInteractPerformed;
            GameManager._inputActions.Player.Melee.performed -= OnInputMeleePerformed;
            GameManager._inputActions.Player.Teleport.performed -= OnInputTeleportPerformed;
            _isInitialized = false;
        }

        private void FixedUpdate()
        { 
            TryMove(_moveVector);
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
            PerformAbility(_abilities[0]);
        }

        private void OnInputTeleportPerformed(InputAction.CallbackContext value)
        {
            Time.timeScale = 0.5f;
        }
        private void OnInputTeleportCanceled(InputAction.CallbackContext value)
        {
            PerformAbility(_abilities[1]);
            Time.timeScale = 1f;
        }
    }
}
