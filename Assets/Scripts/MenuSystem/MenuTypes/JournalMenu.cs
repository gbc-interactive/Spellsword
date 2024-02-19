using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Spellsword
{
    public class JournalMenu : MonoBehaviour, IMenu
    {
        public GameObject _journalMenu;
        public QuestBase _currentQuest = new SampleQuest();
        public TMP_Text _questDescription;
        public TMP_Text _questName;
        public TMP_Text _taskDescription;

        void Start(){
            Disable();
        }
        void UpdateText()
        {
            _questDescription.text = _currentQuest._questDescription;
            _questName.text = _currentQuest._questName;
            _taskDescription.text = "";
            foreach (TaskBase task in _currentQuest._tasks)
            {
                _taskDescription.text += task._taskDescription + ". Remaining: " + task._remaining + "\n";
            }
        }
        public void Enable()
        {
            _journalMenu.SetActive(true);
            UpdateText();
        }

        public void Disable()
        {
            _journalMenu.SetActive(false);
        }
        public void HandleInput()
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

