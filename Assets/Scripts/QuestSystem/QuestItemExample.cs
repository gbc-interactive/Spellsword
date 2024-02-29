using System.Collections;
using System.Collections.Generic;
using Spellsword;
using Unity.VisualScripting;
using UnityEngine;

public class QuestItemExample :MonoBehaviour, IQuestItem
{   
    [SerializeField] private QuestBase _quest;
    [SerializeField] private bool _isInteractable;

    [SerializeField] private string _prompt;

    [SerializeField] private Sprite _inventoryIcon;

    public QuestBase quest { get => _quest; set => SetQuest(_quest); }
    public bool isInteractable { get => _isInteractable; set => SetInteractable(true); }

    public string InteractionPrompt => _prompt;

    public Sprite inventoryIcon { get => _inventoryIcon; set => SetIcon(_inventoryIcon); }

    void Start(){
        SetInteractable(true);
    }
    public bool Interact(InteractionSystem interactor)
    {
        if(_isInteractable){

            //delete this item and add it to the inventory
            QuestActions.AddIntentoryItem(this);
            Destroy(gameObject);
            return true;
        }
        else{
            return false;
        }
    }

    public void SetIcon(Sprite icon)
    {
        _inventoryIcon = icon;
    }

    public void SetInteractable(bool value)
    {
        _isInteractable = value;
    }

    public void SetQuest(QuestBase quest)
    {
        _quest = quest;
    }

    
}
