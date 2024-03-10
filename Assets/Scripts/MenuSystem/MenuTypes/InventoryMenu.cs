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
        
        public List<IQuestItem> _inventoryItems;

        public int _coins = 100;

        public GameObject _inventoryGrid;

        public GameObject _prefab;
        
        void Start(){
            Disable();
            QuestActions.AddIntentoryItem+=AddItem;
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
        public void AddItem(IQuestItem item){
            var newIcon = Instantiate(_prefab, _inventoryGrid.transform);
            newIcon.GetComponent<Image>().sprite = item.inventoryIcon;
            _inventoryItems.Add(item);
            
            //add icon and shit
        }
    }
}