using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public class MeleePotion : MonoBehaviour, IConsumable
{
    [SerializeField] private Sprite _inventoryIcon=Resources.Load<Sprite>("Menu/Spell_Icon_Fireball");
    [SerializeField] private int _shopPrice = 50;
    [SerializeField] private string _itemDescription = "2X your damage!";
    [SerializeField] private string _itemName = "Melee potion";
    [SerializeField] private bool _isInteractable = true;
    [SerializeField] private string _interactionPrompt = "Melee potion";
    [SerializeField] private int _uses = 1;

    [SerializeField] private bool _isActive = false;
    
    public Sprite inventoryIcon { get => _inventoryIcon; set => _inventoryIcon = value; }
    public int shopPrice { get => _shopPrice; set => _shopPrice = value; }
    public string description { get => _itemDescription; set => _itemDescription = value; }
    public string itemName { get => _itemName; set => _itemName = value; }
    public bool isInteractable { get => _isInteractable; set => _isInteractable = value; }

    public string InteractionPrompt => _interactionPrompt;

    public int Uses { get => _uses; set => _uses = value; }
    public bool isActive { get => _isActive; set => _isActive = value; }


    public bool Interact(InteractionSystem interactor)
    {
        if(_isInteractable){

            //delete this item and add it to the inventory
            Debug.Log("Melee potion interact");
            QuestActions.AddIntentoryItem(this);
            Destroy(gameObject);
            return true;
        }
        else{
            return false;
        }
    }

    public void Consume()
    {
        if(!_isActive){
            //find players stats.
            //edit stats that are applicable for this potion
            FindObjectOfType<PlayerController>().damageStat*=2.0f; 
            _isActive = true;
            _uses--;
        }
    }
    public void RemoveEffect()
    {
        if(_isActive){
            FindAnyObjectByType<PlayerController>().damageStat/=2.0f;
            _isActive = false;
        }
    }

    public void SetInteractable(bool value)
    {
        _isInteractable = value;
    }

    public void OnClick()
    {
        Consume();
    }
}
