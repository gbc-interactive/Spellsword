using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class OptionsMenu : MonoBehaviour,IMenu
    {
        public GameObject _optionsMenu;
        void Start(){
            Disable();
        }
        public void Enable()
        {
            _optionsMenu.SetActive(true);
        }

        public void Disable()
        {
            _optionsMenu.SetActive(false);
        }
        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                MenuManager.Instance.ChangeMenu(FindObjectOfType<JournalMenu>());
            }
        }

        
    }
}