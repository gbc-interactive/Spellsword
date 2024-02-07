using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class OptionsMenu : MenuBase
    {
        public GameObject _optionsMenu;

        public override void Enable()
        {
            _optionsMenu.SetActive(true);
            bIsActive = true;
        }

        public override void Disable()
        {
            _optionsMenu.SetActive(false);
            bIsActive = false;
        }
        public override void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                MenuManager.Instance.ChangeMenu(FindObjectOfType<JournalMenu>());
            }
        }

        
    }
}