using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Spellsword
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance = null;
        public static GameManager Instance => _instance;

        public static InputActions _inputActions = null; //editor script

        public PlayerController _playerController = null;
        public InteractionSystem _interactionSystem = null;
        public HUDSystem _hudSystem = null;

        private void Awake()
        {
            _instance = this;

            _inputActions = new InputActions();
            _hudSystem.Initialize();
            _playerController.Initialize();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }
    }
}

