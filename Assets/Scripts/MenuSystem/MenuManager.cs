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
        public IMenu _currentMenu;

        [SerializeField] private GameObject _menuPanel;
        
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
            _currentMenu = FindObjectOfType<JournalMenu>();
        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _bIsOpen = !_bIsOpen;
                if (_bIsOpen)
                    Enable();
                else
                    Disable();
            }
            if(_bIsOpen){
                _currentMenu.HandleInput();
            }
        }
        void Enable()
        {
            _menuPanel.SetActive(true);
            _currentMenu.Enable();
            Time.timeScale = 0;
            _bIsOpen = true;
        }

        void Disable()
        {
            _menuPanel.SetActive(false);
            _currentMenu.Disable();
            Time.timeScale = 1;
            _bIsOpen = false;
        }

        public void ChangeMenu(IMenu menu)
        {
            _currentMenu.Disable();
            _currentMenu = menu;
            Enable();
        }

    }
}

