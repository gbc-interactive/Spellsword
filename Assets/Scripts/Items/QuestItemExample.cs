using System.Collections;
using System.Collections.Generic;
using Spellsword;
using Unity.VisualScripting;
using UnityEngine;

public class QuestItemExample :MonoBehaviour, IItem
{   
    [SerializeField] private bool _isInteractable;

    [SerializeField] private string _prompt;

    [SerializeField] private Sprite _inventoryIcon;

    [SerializeField] private int _shopPrice = 50;

    [SerializeField] private string _itemDescription = "Sample Item Description";

    [SerializeField] private string _itemName = "Sample Item Name";

    public bool isInteractable { get => _isInteractable; set => SetInteractable(true); }

    public string InteractionPrompt => _prompt;

    public Sprite inventoryIcon { get => _inventoryIcon; set => _inventoryIcon = value; }
    public int shopPrice { get => _shopPrice; set => _shopPrice = value; }
    public string description { get => _itemDescription; set => _itemDescription = value;}

    public string itemName { get => _itemName; set => _itemName = value; }

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

    public void SetInteractable(bool value)
    {
        _isInteractable = value;
    }

    public void OnClick()
    {
        throw new System.NotImplementedException();
    }
}
