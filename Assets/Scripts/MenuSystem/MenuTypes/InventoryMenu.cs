using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Spellsword
{
    public class InventoryMenu : MenuBase
    {
        public GameObject _inventoryMenu;
        
        public List<GameObject> _inventoryItems;

        public GameObject _inventoryGrid;

        public GameObject _prefab;

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
        public void AddItem(GameObject item){
            _inventoryItems.Add(item);
            var newIcon = Instantiate(_prefab, _inventoryGrid.transform);
            newIcon.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
            
            //add icon and shit
        }
    }
}