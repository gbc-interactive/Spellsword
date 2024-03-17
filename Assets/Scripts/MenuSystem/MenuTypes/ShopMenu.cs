using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Spellsword;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour, IMenu
{
    [SerializeField] private GameObject _shopMenu;

    [SerializeField] public List<IItem> _shopItems = new List<IItem>();

    [SerializeField] private TMP_Text _coinText;

    [SerializeField] private InventoryMenu _inventoryMenu;

    [SerializeField] private GameObject _itemGrid;

    [SerializeField] private GameObject _shopEntryPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _inventoryMenu = FindObjectOfType<InventoryMenu>();

        _shopItems.Add(new QuestItemExample());
        _shopItems.Add(new Weapon());
        _shopItems.Add(new MeleePotion()); 

        _shopMenu.SetActive(false);
        foreach (IItem shopitem in _shopItems)
        {
            Debug.Log("Creatiing Shop item");
            var newItem = Instantiate(_shopEntryPrefab, _itemGrid.transform);
            newItem.transform.Find("Background/BuyButton").gameObject.GetComponentInChildren<TMP_Text>().text = shopitem.shopPrice.ToString();
            newItem.transform.Find("Background/ItemImage").gameObject.GetComponentInChildren<Image>().sprite = shopitem.inventoryIcon;
            newItem.GetComponentInChildren<TMP_Text>().text = shopitem.itemName + "\n" + shopitem.description; 
            newItem.GetComponentInChildren<Button>().onClick.AddListener(()=>BuyItem(shopitem));
        }
    }
    public void Enable()
    {
        _shopMenu.SetActive(true);
        _coinText.text = "Your Coins: " + _inventoryMenu._coins;
        
    }
    public void Disable()
    {
        MenuManager.Instance._currentMenu = FindObjectOfType<JournalMenu>();
        _shopMenu.SetActive(false);
    }


    public void HandleInput()
    {
    }
    public void Test(){
        Debug.Log("click test");
    }
    public void BuyItem(IItem item)
    {
        if(_inventoryMenu._coins-item.shopPrice>=0){
            Debug.Log("Buy item " + item.itemName);
            QuestActions.AddIntentoryItem(item);
            _shopItems.Remove(item);
            _inventoryMenu._coins-=item.shopPrice;
            _coinText.text = "Your coins: " + _inventoryMenu._coins;
        }
        else{
            Debug.Log("Not enough coins");
        }

        //find shop menu grid slot

        //remove from shop
        //add to inventory
    }
}
