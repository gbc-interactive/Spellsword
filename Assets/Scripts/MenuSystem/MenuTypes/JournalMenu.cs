using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class JournalMenu : MenuBase
    {
        public GameObject _journalMenu;

        public override void Enable()
        {
            _journalMenu.SetActive(true);
        }

        public override void Disable()
        {
            _journalMenu.SetActive(false);
        }
        public override void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                MenuManager.Instance.ChangeMenu(FindObjectOfType<InventoryMenu>());
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                MenuManager.Instance.ChangeMenu(FindObjectOfType<OptionsMenu>());
            }
        }
        
        
    }
}

