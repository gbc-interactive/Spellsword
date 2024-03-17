using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Spellsword
{
    public class JournalMenu : MonoBehaviour, IMenu
    {
        public GameObject _journalMenu;
        
        public List<QuestBase> _questList = new List<QuestBase>();

        public GameObject _questEntryPrefab;
        public GameObject _taskEntryPrefab;
        public GameObject _questGrid;

        void Start()
        {
            Disable();
            _questList.Add(new SampleQuest());
            //add more here
            foreach (var quest in _questList)
            {
                var newQuestEntry = Instantiate(_questEntryPrefab, _questGrid.transform);
                newQuestEntry.GetComponentInChildren<TMP_Text>().text = quest._questDescription;
                foreach (var task in quest._tasks)
                {
                    var newTaskEntry = Instantiate(_taskEntryPrefab, newQuestEntry.GetComponentInChildren<VerticalLayoutGroup>().transform);
                    newTaskEntry.GetComponentInChildren<TMP_Text>().text = task._taskDescription + ". Remaining " + task._remaining;
                }
                
            }
            
        }
        void UpdateText()
        {
            //_questDescription.text = _currentQuest._questDescription;
            //_questName.text = _currentQuest._questName;
            //_taskDescription.text = "";
            //foreach (TaskBase task in _currentQuest._tasks)
            //{
              //  _taskDescription.text += task._taskDescription + ". Remaining: " + task._remaining + "\n";
            //}
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

