using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Spellsword
{
    public class InventoryMenu : MonoBehaviour, IMenu
    {
        public GameObject _inventoryMenu;
        
        public int _maxInventorySlots = 10;
        public List<IItem> _inventoryItems = new List<IItem>();

        public int _coins = 100;
        public GameObject _inventoryGrid;

        public GameObject _prefab;

        
        void Start(){
            Disable();
            QuestActions.AddIntentoryItem+=AddItem;
            for (int i = 0; i < _maxInventorySlots; i++)
            {
               var newIcon = Instantiate(_prefab,_inventoryGrid.transform);

            }
        }
        public void Enable()
        {
            _inventoryMenu.SetActive(true);
            
        }

        public void Disable()
        {
            _inventoryMenu.SetActive(false);
        }

        // Update is called once per frame
        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MenuManager.Instance.ChangeMenu(FindObjectOfType<JournalMenu>());
            }
        }
        public void AddItem(IItem item){
            Debug.Log("Adding " + item.itemName);
            _inventoryGrid.GetComponentsInChildren<Image>()[_inventoryItems.Count].sprite = item.inventoryIcon;
            _inventoryItems.Add(item);
        }
    }
}