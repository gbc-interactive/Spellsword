using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public class Weapon : MonoBehaviour, IEquipment
{
    [SerializeField] private bool _isEquipped = false;
    [SerializeField] private Sprite _inventoryIcon = Resources.Load<Sprite>("Menu/Spell_Icon_Melee");
    [SerializeField] private int _shopPrice = 100;
    [SerializeField] private string _itemDescription = "Adds 10 to your damage stat!";
    [SerializeField] private string _itemName = "Weapon";

    [SerializeField] private bool _isInteractable;

    [SerializeField] private string _interactionPrompt;
    public bool isEquipped { get => _isEquipped; set => _isEquipped = value; }
    public Sprite inventoryIcon { get => _inventoryIcon; set => _inventoryIcon = value; }
    public int shopPrice { get => _shopPrice; set => _shopPrice = value; }
    public string description { get => _itemDescription; set => _itemDescription = value; }
    public string itemName { get => _itemName; set => _itemName = value; }
    public bool isInteractable { get => _isInteractable; set => _isInteractable = value; }

    public string InteractionPrompt => _interactionPrompt;

    public void Equip()
    {
        FindAnyObjectByType<PlayerController>().damageStat+=10.0f;
        _isEquipped = true;
    }

    public bool Interact(InteractionSystem interactor)
    {
        if(_isInteractable){

            //delete this item and add it to the inventory
            QuestActions.AddIntentoryItem(this);
            Debug.Log("Weapon Interact");
            Destroy(gameObject);
            return true;
        }
        else{
            return false;
        }
    }

    public void OnClick()
    {
       if(_isEquipped){
        Unequip();
       }
       else{
        Equip();
       }
    }

    public void SetInteractable(bool value)
    {
        _isInteractable = value;
    }

    public void Unequip()
    {
        FindAnyObjectByType<PlayerController>().damageStat-=10.0f;
        _isEquipped = false;
    }
}
