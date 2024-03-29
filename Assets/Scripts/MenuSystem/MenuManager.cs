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
        [SerializeField] private Button _quitButton;
        public bool _bIsOpen = false;

        public Texture2D _cursor;

        private void Start()
        {
            Cursor.SetCursor(_cursor, Vector2.zero, CursorMode.Auto);
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
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    Enable();
                }
                else
                {
                    Cursor.SetCursor(_cursor, Vector2.zero, CursorMode.Auto);
                    Disable();
                }

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
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        void Disable()
        {
            _menuPanel.SetActive(false);
            _currentMenu.Disable();
            Time.timeScale = 1;
            _bIsOpen = false;
            Cursor.SetCursor(_cursor, Vector2.zero, CursorMode.Auto);
        }

        public void ChangeMenu(IMenu menu)
        {
            _currentMenu.Disable();
            _currentMenu = menu;
            Enable();
        }

    }
}

