using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spellsword
{
    public class InventoryMenu : MenuBase
    {
        public GameObject _inventoryMenu;

        public override void Enable()
        {
            _inventoryMenu.SetActive(true);
        }

        public override void Disable()
        {
            _inventoryMenu.SetActive(false);
        }

        // Update is called once per frame
        public override void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MenuManager.Instance.ChangeMenu(FindObjectOfType<JournalMenu>());
            }
        }
        
    }
}