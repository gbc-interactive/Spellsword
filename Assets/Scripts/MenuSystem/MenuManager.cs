using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Spellsword
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance { get; private set; }
        public MenuBase _currentMenu;
        public GameObject _menuPanel;
        public Button _quitButton;
        private bool _bIsOpen = false;

        private void Start()
        {
            Disable();
        }

        private void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }

        void Update()
        {
            _currentMenu.HandleInput();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _bIsOpen = !_bIsOpen;
                if (_bIsOpen)
                    Enable();
                else
                    Disable();
            }
        }
        void Enable()
        {
            _menuPanel.SetActive(true);
            _currentMenu.Enable();
            Time.timeScale = 0;
        }

        void Disable()
        {
            _menuPanel.SetActive(false);
            _currentMenu.Disable();
            Time.timeScale = 1;
        }

        public void ChangeMenu(MenuBase menu)
        {
            _currentMenu.Disable();
            _currentMenu = menu;
            _currentMenu.Enable();
        }
        public void QuitGame()
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}

